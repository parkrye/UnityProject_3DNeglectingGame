using UnityEngine;

public class BTA_AttackClose : BTAction
{
    private float _attackCooltime = 0f;

    public override bool Work()
    {
        var enemies = Global.CurrentStage.Enemies;
        if (enemies.Count == 0)
            return false;

        var player = Global.CurrentStage.PlayerActor;
        switch (_state)
        {
            case ActionState.Ready:
                _state = ActionState.Working;
                _attackCooltime += player.Data.AttackSpeed;
                break;
            case ActionState.Working:
                _attackCooltime += Time.deltaTime;
                if (_attackCooltime >= player.Data.AttackSpeed)
                {
                    _attackCooltime = 0f;
                    foreach(var target in Physics.OverlapSphere(player.transform.position + Vector3.up * 1f + player.transform.forward, 1f))
                    {
                        var enemy = target.GetComponent<EnemyActor>();
                        if (enemy != null)
                        {
                            enemy.Hit(player.Data.AttackDamage);
                        }
                    }
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
        _attackCooltime = Global.Datas.UserData.ActorData.AttackSpeed;
    }
}
