using Cysharp.Threading.Tasks;
using UnityEngine;

public class BTA_CloseAttack : BTAction
{
    private Transform _enemyTransform;
    private bool _isAttackable = false;

    public override bool Work()
    {
        var enemies = G.CurrentStage.Enemies;
        if (enemies.Count == 0)
            return false;

        var player = G.CurrentStage.PlayerActor;
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
                _isAttackable = true;
                break;
            case ActionState.Working:
                if(_enemyTransform.gameObject.activeInHierarchy == false)
                {
                    _state = ActionState.End;
                    return false;
                }
                if (Vector3.SqrMagnitude(player.transform.position - _enemyTransform.position) < G.V.SquareCloseAttackRange)
                {
                    player.LookAt(_enemyTransform.position);
                    player.Anim.StopMoveAnimation();

                    if (_isAttackable)
                    {
                        _isAttackable = false;
                        player.Anim.AttackEndEvent.AddListener(AttackReaction);
                        player.Anim.PlayAttackAnimation();
                    }
                }
                else
                {
                    player.Anim.PlayMoveAnimation();
                    player.SetDestination(_enemyTransform.position);
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
        await UniTask.Delay(G.V.AttackDelayTime / G.CurrentStage.PlayerActor.GetStatus(Status.AttackSpeed));
        _isAttackable = true;
    }

    private void AttackReaction()
    {
        var player = G.CurrentStage.PlayerActor;
        player.Anim.AttackEndEvent.RemoveListener(AttackReaction);

        var colliders = Physics.OverlapSphere(player.transform.position + player.transform.forward, G.V.CloseAttackRange, 1 << 10);
        foreach (var target in colliders)
        {
            var enemy = target.GetComponent<EnemyActor>();
            if (enemy != null)
            {
                enemy.Hit(player.GetStatus(Status.AttackDamage));
                break;
            }
        }

        AttackCoolTimeRoutine().Forget();
    }
}
