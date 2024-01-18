using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTA_AttackClose : BTAction
{
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

                break;
            case ActionState.Working:

                break;
            case ActionState.End:
                return true;
        }

        return false;
    }
}
