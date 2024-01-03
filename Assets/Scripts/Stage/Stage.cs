using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public enum StageState
{
    Ready,
    Creating,
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

    public StageState _state;
    
    private Dictionary<int, Actor> _playerParty = new Dictionary<int, Actor>();
    private ObjectPool<Actor> _enemyPool;

    private void Awake()
    {
        _actorCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        _mainLight = GetComponentInChildren<Light>();

        _startPosition = GameObject.Find("StartPosition").transform.position;
        _actorParent = GameObject.Find("Actors").transform;

        _state = StageState.Ready;

        _enemyPool = new ObjectPool<Actor>(
            createFunc: CreateFunc,
            actionOnGet: OnGetActor,
            actionOnRelease: OnReleaseActor,
            actionOnDestroy: OnDestroyActor);
    }

    public void SpawnActor(Actor actor, ActorType actorType, Vector3 spawnPosition)
    {
        switch(actorType)
        {
            case ActorType.PC:
                break;
            case ActorType.NPC:
                break;
            case ActorType.Enemy:
                break;
            case ActorType.Boss:
                break;
        }
    }

    private Actor CreateFunc()
    {
        var actorObject = new Actor();
        return actorObject;
    }

    private void OnGetActor(Actor actor)
    {

    }

    private void OnReleaseActor(Actor actor)
    {

    }

    private void OnDestroyActor(Actor actor)
    {

    }
}
