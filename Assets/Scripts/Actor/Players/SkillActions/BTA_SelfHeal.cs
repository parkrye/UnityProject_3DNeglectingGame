using Cysharp.Threading.Tasks;

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
                    player.Anim.DizzyEndEvent.AddListener(HealReaction);
                    player.Anim.PlayDizzyAnimation();
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
        player.Anim.DizzyEndEvent.RemoveListener(HealReaction);

        player.Hit(-player.GetStatus(Status.Hp) * 0.5f);

        CoolTimeRoutine().Forget();
    }
}
