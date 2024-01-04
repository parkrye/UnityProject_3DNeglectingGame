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
    private Pool _actorPool = new Pool();

    private void Awake()
    {
        _actorCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        _mainLight = GetComponentInChildren<Light>();

        _startPosition = GameObject.Find("StartPosition").transform.position;
        _actorParent = GameObject.Find("Actors").transform;

        _state = StageState.Ready;
    }

    public void SpawnActor(Actor actor, Vector3 spawnPosition, Quaternion rotation)
    {
        _actorPool.Get(actor, spawnPosition, rotation, _actorParent);
    }
}
