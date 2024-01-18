using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class PlayerActor : Actor
{
    private PlayerActionHandler _actionHandler;
    private NavMeshAgent _navMesh;

    private void Awake()
    {
        _actionHandler = GetComponent<PlayerActionHandler>();
        if(_actionHandler == null)
            _actionHandler = gameObject.AddComponent<PlayerActionHandler>();
        _type = ActorType.PC;
        _state = ActorState.Ready;
    }

    public void StageStarted()
    {
        _state = ActorState.Alive;
        ActionRoutine().Forget();

        _navMesh = gameObject.AddComponent<NavMeshAgent>();
        _navMesh.stoppingDistance = 2f;
    }

    private async UniTask ActionRoutine()
    {
        await UniTask.WaitUntil(() => _state == ActorState.Alive);

        while (_state == ActorState.Alive)
        {
            _actionHandler.Work();
            await UniTask.WaitForFixedUpdate();
        }
    }

    public void SetDestination(Vector3 position)
    {
        if (_navMesh == null)
            return;

        _navMesh.SetDestination(position);
    }
}
