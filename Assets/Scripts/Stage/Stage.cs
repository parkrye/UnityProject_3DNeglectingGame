using Cinemachine;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum StageState
{
    Ready,
    Battle,
    Dead,
    Boss,
    Clear,
}

public class Stage : MonoBehaviour
{
    private CinemachineVirtualCamera _actorCamera;
    private Light _mainLight;

    private Vector3 _startPosition;
    private Transform _actorParent;

    public UnityEvent AllPCActorSpawned = new UnityEvent();
    public UnityEvent<ActorType> ActorDie = new UnityEvent<ActorType>();

    public StageState _state = StageState.Ready;

    private PlayerActor _playerActor;
    public PlayerActor PlayerActor { get { return _playerActor; } }
    private Pool _actorPool = new Pool();

    private List<EnemyActor> _enemyOriginalList = new List<EnemyActor>();
    private Vector3[] _enemySpawnPositionArray;
    private int _enemySpawnDelay = 10000;
    private int _emenySpawnCount = 100;

    private List<EnemyActor> _spawnedEnemyList = new List<EnemyActor>();
    public List<EnemyActor> Enemies { get { return _spawnedEnemyList; } }

    private int _stageLevel;
    public int StageLevel { get { return _stageLevel; } }

    public void Initialize()
    {
        Global.CurrentStage = this;

        _actorCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        _mainLight = GetComponentInChildren<Light>();

        _startPosition = GameObject.Find("StartPosition").transform.position;
        _actorParent = GameObject.Find("Actors").transform;

        var enemySpawnPositionParent = GameObject.Find("SpawnPositions");
        var enemySpawnPositions = enemySpawnPositionParent.GetComponentsInChildren<Transform>();
        _enemySpawnPositionArray = enemySpawnPositions
            .Select(t => t.transform.position)
            .Where(t => t.Equals(enemySpawnPositionParent) == false)
            .ToArray();

        EnemySpawnRoutine().Forget();

        var navMeshSurfece = GetComponentInChildren<NavMeshSurface>();
        navMeshSurfece.BuildNavMesh();

        Global.UI.CloseAllDialog();
    }

    public void SpawnPlayer(PlayerActor actor)
    {
        var player = _actorPool.Get(actor, _startPosition, Quaternion.identity, _actorParent);
        _playerActor = player;
        _actorCamera.Follow = _playerActor.transform;
        _actorCamera.LookAt = _playerActor.transform;
        _playerActor.Anim.PlayRecoveryAnimation();
        _playerActor.DieEvent.AddListener(ResetStage);
        AllPCActorSpawned.AddListener(_playerActor.StageStarted);
    }

    private EnemyActor SpawnEnemy(EnemyActor actor, Vector3 spawnPosition, Quaternion rotation)
    {
        if (_state == StageState.Dead || _state == StageState.Clear)
            return null;

        var spawned = _actorPool.Get(actor, spawnPosition, rotation, _actorParent);
        spawned.Init(_stageLevel);
        return spawned;
    }

    public void AddEnemyActor(EnemyActor enemy)
    {
        _enemyOriginalList.Add(enemy);
    }

    private async UniTask EnemySpawnRoutine()
    {
        await UniTask.WaitUntil(() => _state == StageState.Battle);

        var enemyCount = 0;

        while (_state == StageState.Battle)
        {
            var enemyIndex = Random.Range(0, _enemyOriginalList.Count);
            var enemyPosition = Random.Range(0, _enemySpawnPositionArray.Length);
            if(enemyCount < _emenySpawnCount)
            {
                var enemy = SpawnEnemy(_enemyOriginalList[enemyIndex], _enemySpawnPositionArray[enemyPosition], Quaternion.identity);
                enemy.EnemyDie.RemoveAllListeners();
                enemy.EnemyDie.AddListener(EnemyDied);
                _spawnedEnemyList.Add(enemy);
                enemyCount++;
            }
            await UniTask.Delay(_enemySpawnDelay);
        }
    }

    public void EndSetting()
    {
        _state = StageState.Battle;
        AllPCActorSpawned?.Invoke();
        AllPCActorSpawned.RemoveAllListeners();
    }

    private void EnemyDied(EnemyActor enemy)
    {
        Global.Datas.User.AddCurrency(CurrencyType.Gold, 1);
        Global.Datas.User.AddCurrency(CurrencyType.Exp, 1);
        foreach(var reward in enemy.Data.RewardData.Rewards)
        {
            Global.Datas.User.AddCurrency((CurrencyType)reward.Id, reward.Count);
        }

        _spawnedEnemyList.Remove(enemy);
        _actorPool.Release(enemy);
    }

    private void ResetStage()
    {
        for(int i = 0; i < _spawnedEnemyList.Count; i++)
        {
            _actorPool.Release(_spawnedEnemyList[i]);
        }

        _actorPool.Release(_playerActor);

        _actorPool.Reset();

        SceneManager.LoadScene("MainScene");
    }

    private void OnApplicationQuit()
    {
        Global.Datas.User.SaveData();
    }
}
