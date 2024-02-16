using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class BTA_SelfHeal : BTAction
{
    private bool _isHealable = false;

    public override bool Work()
    {
        var player = G.CurrentStage.PlayerActor;
        switch (_state)
        {
            case ActionState.Ready:
                _state = ActionState.Working;
                _isHealable = true;
                break;
            case ActionState.Working:
                if (_isHealable && player.IsDamaged)
                {
                    _isHealable = false;
                    player.Anim.AttackEndEvent.AddListener(HealReaction);
                    player.Anim.PlayAttackAnimation();
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
        _isHealable = true;
    }

    private async UniTask CoolTimeRoutine()
    {
        await UniTask.Delay(G.V.AttackDelayTime / G.CurrentStage.PlayerActor.GetStatus(Status.AttackSpeed));
        _isHealable = true;
    }

    private void HealReaction()
    {
        var player = G.CurrentStage.PlayerActor;
        player.Anim.AttackEndEvent.RemoveListener(HealReaction);

        player.Hit(-player.GetStatus(Status.Hp) * 0.5f);

        CoolTimeRoutine().Forget();
    }
}
