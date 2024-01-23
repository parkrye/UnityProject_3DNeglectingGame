using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class PlayerActor : Actor, IHitable
{
    private PlayerActionHandler _actionHandler;
    private NormalAnimationController _anim;
    private NavMeshAgent _navMesh;
    private ActorData _data;
    public ActorData Data { get { return _data; } }
    public NormalAnimationController Anim { get { return _anim; } }

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

    public void StageStarted()
    {
        _state = ActorState.Alive;
        _data = Global.Datas.UserData.ActorData;
        ActionRoutine().Forget();

        _navMesh = gameObject.AddComponent<NavMeshAgent>();
        _navMesh.stoppingDistance = 3f;
        _navMesh.speed = _data.MoveSpeed;
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
        return true;
    }
}
