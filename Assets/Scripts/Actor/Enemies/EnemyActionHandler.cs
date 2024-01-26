using UnityEngine;

public class EnemyActionHandler : MonoBehaviour
{
    private EnemyActor _enemyActor;
    private BTBranch _root;

    private void Awake()
    {
        _enemyActor = GetComponent<EnemyActor>();

        _root = new BTBranch(BranchType.OR);

        BTBranch battle = new BTBranch(BranchType.AND);
        battle.AddChild(new BTA_E_CheckBattle(_enemyActor));
        BTBranch battleActions = new BTBranch(BranchType.BOTH);
        battleActions.AddChild(new BTA_E_CloseAttack(_enemyActor));
        battle.AddChild(battleActions);

        _root.AddChild(battle);
        _root.AddChild(new BTA_E_WalkAround());
    }

    public void Work()
    {
        _root.Work();
    }

    public void ResetBT()
    {
        _root.ResetChildren();
    }
}
