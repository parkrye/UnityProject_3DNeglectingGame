using Cinemachine;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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

    private Dictionary<int, EnemyActor> _enemyOriginalList = new Dictionary<int, EnemyActor>();
    private Vector3[] _enemySpawnPositionArray;

    private List<EnemyActor> _spawnedEnemyList = new List<EnemyActor>();
    public List<EnemyActor> Enemies { get { return _spawnedEnemyList; } }

    private int _stageLevel = 1;
    public int StageLevel { get { return _stageLevel; } }

    public void Initialize()
    {
        G.CurrentStage = this;

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

        G.UI.CloseAllDialog();
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

    private EnemyActor SpawnEnemy(int index, EnemyActor actor, Vector3 spawnPosition, Quaternion rotation)
    {
        if (_state == StageState.Dead || _state == StageState.Clear)
            return null;

        var spawned = _actorPool.Get(actor, spawnPosition, rotation, _actorParent);
        spawned.Init(_stageLevel, index);
        return spawned;
    }

    public void AddEnemyActor(int id, EnemyActor enemyprefab)
    {
        if (_enemyOriginalList.ContainsKey(id))
            return;

        _enemyOriginalList.Add(id, enemyprefab);
    }

    private async UniTask EnemySpawnRoutine()
    {
        await UniTask.WaitUntil(() => _state == StageState.Battle);

        while (_state == StageState.Battle)
        {
            var enemyIndex = _enemyOriginalList.Keys.ToList()[Random.Range(0, _enemyOriginalList.Count)];
            var enemyPosition = Random.Range(0, _enemySpawnPositionArray.Length);
            if(_spawnedEnemyList.Count < G.V.SpawnLimit)
            {
                var enemy = SpawnEnemy(enemyIndex, _enemyOriginalList[enemyIndex], _enemySpawnPositionArray[enemyPosition], Quaternion.identity);
                enemy.EnemyDie.RemoveAllListeners();
                enemy.EnemyDie.AddListener(EnemyDied);
                _spawnedEnemyList.Add(enemy);
            }
            await UniTask.Delay(G.V.SpawnDelay);
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
        foreach (var reward in enemy.Data.RewardData.Rewards)
        {
            G.Data.User.AddCurrency(reward.Type, reward.Count);
        }

        enemy.ResetAction();
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

        G.UI.CloseAllDialog();
        G.UI.CloseCurrentView();

        SceneManager.LoadScene("MainScene");
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    public void SaveData()
    {
        var playerDataPath = G.V.DataPath + "PlayerData";
        var playerDataToJson = JsonUtility.ToJson(G.Data.User.PlayerData);
        File.WriteAllText(playerDataPath, playerDataToJson);

        var playerActorDataPath = G.V.DataPath + "PlayerActorData";
        var playerActorDataToJson = JsonUtility.ToJson(G.Data.User.ActorData);
        File.WriteAllText(playerActorDataPath, playerActorDataToJson);

        var itemDataPath = G.V.DataPath + "ItemData";
        var itemDataList = new ItemDataList();
        foreach(var item in G.Data.Item.Items.Values)
        {
            itemDataList.Add(item);
        }
        var itemDataToJson = JsonUtility.ToJson(itemDataList);
        File.WriteAllText(itemDataPath, itemDataToJson);
    }
}
