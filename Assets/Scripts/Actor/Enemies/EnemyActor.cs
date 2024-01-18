using UnityEngine.AI;

public class EnemyActor : Actor
{
    private NavMeshAgent navMesh;

    private void Awake()
    {
        _type = ActorType.Enemy;
    }

    private void OnEnable()
    {
        navMesh = gameObject.AddComponent<NavMeshAgent>();
    }
}
