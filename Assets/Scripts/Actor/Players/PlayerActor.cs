using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class PlayerActor : Actor, IHitable, IFloatPublisher
{
    private PlayerActionHandler _actionHandler;
    private NormalAnimationController _anim;
    private NavMeshAgent _navMesh;
    private ActorData _data;
    private int _hp;
    public bool IsDamaged { get { return _hp < GetStatus(Status.Hp); } }
    public NormalAnimationController Anim { get { return _anim; } }
    public UnityEvent DieEvent = new UnityEvent();
    public UnityEvent<float, bool> HPRatioEvent = new UnityEvent<float, bool>();
    private List<IFloatSubscriber> _damageSubscribers = new List<IFloatSubscriber>();
    private Dictionary<Status, List<IFloatSubscriber>> _statusSubscribers = new Dictionary<Status, List<IFloatSubscriber>>();

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

        _statusSubscribers.Add(Status.MoveSpeed, new List<IFloatSubscriber>());
        _statusSubscribers.Add(Status.AttackSpeed, new List<IFloatSubscriber>());
        _statusSubscribers.Add(Status.AttackDamage, new List<IFloatSubscriber>());
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
        var damageResult = DamagePublish(damage);
        _hp -= (int)damageResult;
        HPRatioEvent?.Invoke((float)_hp / GetStatus(Status.Hp), false);
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

    public void AddSubscriber(IFloatSubscriber subscriber)
    {

    }

    public void RemoveSubscriber(IFloatSubscriber subscriber)
    {

    }

    public void AddDamageSubscriber(IFloatSubscriber subscriber)
    {
        _damageSubscribers.Add(subscriber);
    }

    public void RemoveDamageSubscriber(IFloatSubscriber subscriber)
    {
        _damageSubscribers.Remove(subscriber);
    }

    private float DamagePublish(float origin)
    {
        var result = origin;
        foreach(var subscriber in _damageSubscribers) 
        {
            result = subscriber.Modifiy(origin);
        }
        return result;
    }

    public int GetStatus(Status status)
    {
        var result = 0f;
        switch(status)
        {
            case Status.Hp:
                result = _data.Hp;
                break;
            case Status.MoveSpeed:
                result = StatusPublish(status, _data.MoveSpeed);
                break;
            case Status.AttackSpeed:
                result = StatusPublish(status, _data.AttackSpeed);
                break;
            case Status.AttackDamage:
                result = StatusPublish(status, _data.AttackDamage);
                break;
        }
        return (int)result;
    }

    private float StatusPublish(Status status, float value)
    {
        var result = value;
        var subscribers = _statusSubscribers.Where(t => t.Key.Equals(status)).Select(t => t.Value).First();
        if (subscribers == null)
            return value;

        foreach (var subscriber in subscribers)
        {
            result = subscriber.Modifiy(result);
        }
        return result;
    }
}
