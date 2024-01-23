using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine;

public class EnemyActor : Actor, IHitable
{
    private ActorData _data;
    public ActorData Data { get { return _data; } }
    private int _hp;
    private NavMeshAgent _navMesh;
    private NormalAnimationController _anim;
    public NormalAnimationController Anim { get { return _anim; } }

    public UnityEvent<EnemyActor> EnemyDie = new UnityEvent<EnemyActor>();

    private void Awake()
    {
        _type = ActorType.Enemy;
        _anim = GetComponent<NormalAnimationController>();
        if( _anim == null )
            _anim = gameObject.AddComponent<NormalAnimationController>();
    }

    private void OnEnable()
    {
        if(_data == null)
            _data = Global.Datas.GetEnemyData(1);
        _hp = _data.Hp;
        if (_navMesh == null)
            _navMesh = gameObject.AddComponent<NavMeshAgent>();
    }

    public bool Hit(float damage)
    {
        _hp -= (int)damage;
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
}
