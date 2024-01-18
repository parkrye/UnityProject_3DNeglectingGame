using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTA_MoveToClose : BTAction
{
    private Transform _enemyTransform;

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
                var closestDistance = float.MaxValue;
                var closestEnemy = enemies[0];

                foreach (var enemy in enemies)
                {
                    float distance = Vector3.SqrMagnitude(player.transform.position - enemy.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestEnemy = enemy;
                    }
                }

                _enemyTransform = closestEnemy.transform;
                break;
            case ActionState.Working:
                player.transform.Translate((_enemyTransform.position - player.transform.position).normalized
                    * Global.UD.ActorData.MoveSpeed * 0.01f);
                break;
            case ActionState.End:
                break;
        }

        return true;
    }
}
