using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyActor : Actor, IHitable
{
    private ActorData _data;
    private int _hp;
    private NavMeshAgent _navMesh;

    public UnityEvent<EnemyActor> EnemyDie = new UnityEvent<EnemyActor>();

    private void Awake()
    {
        _type = ActorType.Enemy;
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
        if(_hp <= 0)
        {
            EnemyDie?.Invoke(this);
            _state = ActorState.Dead;
        }
        return true;
    }
}
