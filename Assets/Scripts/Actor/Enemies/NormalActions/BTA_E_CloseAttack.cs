using Cysharp.Threading.Tasks;
using UnityEngine;

public class BTA_E_CloseAttack : BTAction
{
    private EnemyActor _enemyActor;
    private bool _isAttackable = false;

    public BTA_E_CloseAttack(EnemyActor enemyActor)
    {
        _enemyActor = enemyActor;
    }

    public override bool Work()
    {
        var player = Global.CurrentStage.PlayerActor;
        if (player == null)
            return false;

        switch (_state)
        {
            case ActionState.Ready:
                _state = ActionState.Working;
                AttackCoolTimeRoutine().Forget();
                break;
            case ActionState.Working:
                if (_isAttackable)
                {
                    _state = ActionState.End;
                    _isAttackable = true;
                    _enemyActor.Anim.AttackEndEvent.AddListener(AttackReaction);
                    _enemyActor.Anim.PlayAttackAnimation();
                }
                _enemyActor.LookAt(player.transform.position);
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
        await UniTask.Delay(1000 / _enemyActor.Data.AttackSpeed);
        _isAttackable = true;
    }

    private void AttackReaction()
    {
        _enemyActor.Anim.AttackEndEvent.RemoveAllListeners();
        var colliders = Physics.OverlapSphere(_enemyActor.transform.position + _enemyActor.transform.forward, 3f, 1 << 9);
        foreach (var target in colliders)
        {
            var player = target.GetComponent<PlayerActor>();
            if (player != null)
            {
                player.Hit(_enemyActor.Data.AttackDamage);
            }
        }
    }
}
