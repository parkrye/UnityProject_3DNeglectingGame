using Cinemachine;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

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
    private Pool _actorPool = new Pool();

    private List<EnemyActor> _enemyPathList = new List<EnemyActor>();
    private Vector3[] _enemySpawnPositionArray;
    private int _enemySpawnDelay = 10000;
    private int _emenySpawnCount = 100;

    public void Initialize()
    {
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

        _state = StageState.Battle;

        EnemySpawnRoutine().Forget();

        var navMeshSurfece = GetComponentInChildren<NavMeshSurface>();
        navMeshSurfece.BuildNavMesh();
    }

    public void SpawnPlayer(PlayerActor actor)
    {
        SpawnActor(actor, _startPosition, Quaternion.identity);

        _playerActor = actor;
        _actorCamera.m_Follow = actor.transform;
        _actorCamera.m_LookAt = actor.transform;
    }

    private void SpawnActor(Actor actor, Vector3 spawnPosition, Quaternion rotation)
    {
        if (_state != StageState.Battle)
            return;

        _actorPool.Get(actor, spawnPosition, rotation, _actorParent);
    }

    public void AddEnemyActor(EnemyActor enemy)
    {
        _enemyPathList.Add(enemy);
    }

    private async UniTask EnemySpawnRoutine()
    {
        await UniTask.WaitUntil(() => _state == StageState.Battle);

        var enemyCount = 0;

        while (_state == StageState.Battle)
        {
            var enemyIndex = Random.Range(0, _enemyPathList.Count + 1);
            var enemyPosition = Random.Range(0, _enemySpawnPositionArray.Length + 1);
            if(enemyCount < _emenySpawnCount)
            {
                try
                {
                    SpawnActor(_enemyPathList[enemyIndex], _enemySpawnPositionArray[enemyPosition], Quaternion.identity);
                    enemyCount++;
                }
                catch
                {

                }
            }
            await UniTask.Delay(_enemySpawnDelay);
        }
    }
}
