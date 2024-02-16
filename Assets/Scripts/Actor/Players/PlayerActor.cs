using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class PlayerActor : Actor, IHitable, IDamagePublisher
{
    private PlayerActionHandler _actionHandler;
    private NormalAnimationController _anim;
    private NavMeshAgent _navMesh;
    private ActorData _data;
    public ActorData Data { get { return _data; } }
    private int _hp;
    public bool IsDamaged { get { return _hp < Data.Hp; } }
    public NormalAnimationController Anim { get { return _anim; } }
    public UnityEvent DieEvent = new UnityEvent();
    public UnityEvent<float, bool> HPRatioEvent = new UnityEvent<float, bool>();
    private List<IDamageSubscriber> _subscribers = new List<IDamageSubscriber>();

    private void Awake()
    {
        _actionHandler = GetComponent<PlayerActionHandler>();
        if(_actionHandler == null)
            _actionHandler = gameObject.AddComponent<PlayerActionHandler>();
        _anim = GetComponent<NormalAnimationController>();
        if(_anim == null)
            _anim = gameObject.AddComponent<NormalAnimationController>();

        _type = ActorType.PC;
        _state = ActorState.Ready;
    }

    private void OnDisable()
    {
        _actionHandler.ResetBT();
    }

    public void StageStarted()
    {
        _state = ActorState.Alive;
        _data = G.Data.User.ActorData;
        _hp = _data.Hp;
        ActionRoutine().Forget();

        _navMesh = gameObject.AddComponent<NavMeshAgent>();
        _navMesh.stoppingDistance = 4f;
        _navMesh.speed = _data.MoveSpeed;
        HPRatioEvent?.Invoke(1f, false);
    }

    private async UniTask ActionRoutine()
    {
        await UniTask.WaitUntil(() => _state == ActorState.Alive);

        while (_state == ActorState.Alive)
        {
            _actionHandler.Work();
            await UniTask.WaitForEndOfFrame();
        }
    }

    public void SetDestination(Vector3 position)
    {
        if (_navMesh == null)
            return;

        _navMesh.SetDestination(position);
    }
    
    public void LookAt(Vector3 position)
    {
        transform.LookAt(position);
    }

    public bool Hit(float damage)
    {
        var damageResult = Publish(damage);
        _hp -= (int)damageResult;
        HPRatioEvent?.Invoke((float)_hp / Data.Hp, false);
        if (_hp <= 0)
        {
            _state = ActorState.Dead;
            _anim.DieEndEvent.AddListener(DieReaction);
            _anim.PlayDieAnimation();
        }
        else
        {
            _anim.PlayHitAnimation();
        }
        return true;
    }

    private void DieReaction()
    {
        DieEvent?.Invoke();
    }

    public void AddSubscriber(IDamageSubscriber subscriber)
    {
        _subscribers.Add(subscriber);
    }

    public void RemoveSubscriber(IDamageSubscriber subscriber)
    {
        _subscribers.Remove(subscriber);
    }

    public float Publish(float origin)
    {
        var result = origin;
        foreach(var subscriber in _subscribers) 
        {
            result = subscriber.Modifiy(origin);
        }
        return result;
    }
}
