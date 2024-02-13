using Cysharp.Threading.Tasks;
using UnityEngine;

public class BTA_E_CloseAttack : BTAction
{
    private EnemyActor _enemyActor;
    private bool _isAttackable = false;

    public BTA_E_CloseAttack(EnemyActor enemyActor)
    {
        _enemyActor = enemyActor;
        _enemyActor.Anim.AttackEndEvent.AddListener(AttackReaction);
    }

    public override bool Work()
    {
        var player = G.CurrentStage.PlayerActor;
        if (player == null)
            _state = ActionState.End;

        switch (_state)
        {
            case ActionState.Ready:
                if (_enemyActor.State == ActorState.Alive)
                {
                    _state = ActionState.Working;
                    AttackCoolTimeRoutine().Forget();
                }
                break;
            case ActionState.Working:
                if (Vector3.SqrMagnitude(player.transform.position - _enemyActor.transform.position) < G.V.SquareCloseAttackRange)
                {
                    _enemyActor.LookAt(player.transform.position);
                    _enemyActor.Anim.StopMoveAnimation();
                    if (_isAttackable)
                    {
                        _isAttackable = false;
                        _enemyActor.Anim.PlayAttackAnimation();
                    }
                    break;
                }
                else
                {
                    _enemyActor.SetDestination(player.transform.position);
                    _enemyActor.Anim.PlayMoveAnimation();
                }
                break;
            case ActionState.End:
                Reset();
                return true;
        }

        return false;
    }

    public override void Reset()
    {
        base.Reset();
        _isAttackable = true;
    }

    private async UniTask AttackCoolTimeRoutine()
    {
        while (_enemyActor.State == ActorState.Alive)
        {
            await UniTask.Delay(G.V.AttackDelayTime / _enemyActor.Data.EnemyActorData.AttackSpeed);
            _isAttackable = true;
        }
    }

    private void AttackReaction()
    {
        var colliders = Physics.OverlapSphere(_enemyActor.transform.position + _enemyActor.transform.forward, G.V.CloseAttackRange, 1 << 9);
        foreach (var target in colliders)
        {
            var player = target.GetComponent<PlayerActor>();
            if (player != null)
            {
                player.Hit(_enemyActor.AttackDamage);
                return;
            }
        }
    }
}
