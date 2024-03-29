using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class EnemyActor : Actor, IHitable
{
    private EnemyData _data;
    public EnemyData Data { get { return _data; } }
    private int _hp, _maxHp, _attackDamage;
    public bool IsDamaged { get { return _hp < _maxHp; } }
    public int AttackDamage { get { return _attackDamage; } }
    public ActorState State { get { return _state; } }
    private EnemyActionHandler _actionHandler;
    private NavMeshAgent _navMesh;
    private NormalAnimationController _anim;
    public NormalAnimationController Anim { get { return _anim; } }

    public UnityEvent<EnemyActor> EnemyDie = new UnityEvent<EnemyActor>();
    public UnityEvent<float, bool> HPRatioEvent = new UnityEvent<float, bool>();

    private void Awake()
    {
        _actionHandler = GetComponent<EnemyActionHandler>();
        _actionHandler ??= gameObject.AddComponent<EnemyActionHandler>();
        _anim = GetComponent<NormalAnimationController>();
        _anim ??= gameObject.AddComponent<NormalAnimationController>();
        var hpBar = GetComponentInChildren<HPBar>();
        if (hpBar != null)
            HPRatioEvent.AddListener(hpBar.ModifyBar);

        _type = ActorType.Enemy;
    }

    private void OnDisable()
    {
        _state = ActorState.Dead;
        _actionHandler.ResetBT();
    }

    public void Init(int stageLevel, int index)
    {
        _data ??= G.Data.Enemy.GetEnemyData(index);
        _maxHp = (_data.EnemyActorData.Hp + _data.EnemyActorData.Level) * stageLevel;
        _hp = _maxHp;
        _attackDamage += _data.EnemyActorData.Level + stageLevel;
        _navMesh ??= gameObject.AddComponent<NavMeshAgent>();
        ActionRoutine().Forget();
        HPRatioEvent?.Invoke(1f, true);
        _anim.PlayRecoveryAnimation();
        _state = ActorState.Alive;
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

    public bool Hit(float damage)
    {
        if (_state != ActorState.Alive)
            return false;

        _hp -= (int)damage;
        HPRatioEvent?.Invoke((float)_hp / _maxHp, true);
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
        EnemyDie?.Invoke(this);
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

    public void ResetAction()
    {
        _actionHandler.ResetBT();
    }
}
