using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyActor : Actor, IHitable
{
    private ActorData _data;
    private NavMeshAgent navMesh;
    public UnityEvent<EnemyActor> EnemyDie = new UnityEvent<EnemyActor>();

    private void Awake()
    {
        _type = ActorType.Enemy;
    }

    private void OnEnable()
    {
        if(_data == null)
            _data = Global.Datas.GetEnemyData(1);
        if(navMesh == null)
            navMesh = gameObject.AddComponent<NavMeshAgent>();
    }

    public bool Hit(float damage)
    {
        Debug.Log($"Hit! {damage}");
        _data.Hp -= (int)damage;
        if(_data.Hp <= 0)
        {
            EnemyDie?.Invoke(this);
            _state = ActorState.Dead;
        }
        return true;
    }
}
