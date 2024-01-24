using Cysharp.Threading.Tasks;
using UnityEngine;

public class BTA_CloseAttack : BTAction
{
    private bool _isAttackable = false;

    public override bool Work()
    {
        var player = Global.CurrentStage.PlayerActor;
        switch (_state)
        {
            case ActionState.Ready:
                _state = ActionState.Working;
                AttackCoolTimeRoutine().Forget();
                break;
            case ActionState.Working:
                if (_args.TArg != null)
                    player.LookAt(_args.TArg.position);

                if (_isAttackable)
                {
                    _state = ActionState.End;
                    _isAttackable = true;
                    player.Anim.AttackEndEvent.AddListener(AttackReaction);
                    player.Anim.PlayAttackAnimation();
                }
                break;
            case ActionState.End:
                return true;
        }

        return false;
    }

    public override void Reset()
    {
        base.Reset();
        _isAttackable = false;
    }

    private async UniTask AttackCoolTimeRoutine()
    {
        _isAttackable = false;
        await UniTask.Delay(1000 / Global.CurrentStage.PlayerActor.Data.AttackSpeed);
        _isAttackable = true;
    }

    private void AttackReaction()
    {
        var player = Global.CurrentStage.PlayerActor;
        player.Anim.AttackEndEvent.RemoveAllListeners();
        var colliders = Physics.OverlapSphere(player.transform.position + player.transform.forward, 3f, 1 << 10);
        foreach (var target in colliders)
        {
            var enemy = target.GetComponent<EnemyActor>();
            if (enemy != null)
            {
                enemy.Hit(player.Data.AttackDamage);
            }
        }
    }
}
