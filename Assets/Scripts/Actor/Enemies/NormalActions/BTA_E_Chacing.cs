using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTA_E_Chacing : BTAction
{
    private EnemyActor _enemyActor;

    public BTA_E_Chacing(EnemyActor enemyActor)
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
                break;
            case ActionState.Working:
                if (Vector3.SqrMagnitude(player.transform.position - _enemyActor.transform.position) < 9f)
                {
                    _state = ActionState.End;
                    _enemyActor.Anim.StopMoveAnimation();
                    break;
                }
                _enemyActor.SetDestination(player.transform.position);
                _enemyActor.Anim.PlayMoveAnimation();
                break;
            case ActionState.End:
                break;
        }

        return _state == ActionState.End;
    }
}
