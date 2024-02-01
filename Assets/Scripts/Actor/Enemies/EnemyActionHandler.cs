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
        battle.AddChild(new BTA_E_CloseAttack(_enemyActor));

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
