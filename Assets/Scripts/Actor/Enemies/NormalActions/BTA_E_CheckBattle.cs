using UnityEngine;

public class BTA_E_CheckBattle : BTAction
{
    private EnemyActor _enemyActor;

    public BTA_E_CheckBattle(EnemyActor enemyActor)
    {
        _enemyActor = enemyActor;
    }

    public override bool Work()
    {
        switch (_state)
        {
            case ActionState.Ready:
                _state = ActionState.Working;
                break;
            case ActionState.Working:
                if (_enemyActor.IsDamaged)
                {
                    _state = ActionState.End;
                    break;
                }

                if (Vector3.SqrMagnitude(G.CurrentStage.PlayerActor.transform.position - _enemyActor.transform.position) < G.V.SquareEnenyFindRange)
                {
                    _state = ActionState.End;
                    break;
                }
                break;
            case ActionState.End:
                break;
        }

        return _state == ActionState.End;
    }
}
