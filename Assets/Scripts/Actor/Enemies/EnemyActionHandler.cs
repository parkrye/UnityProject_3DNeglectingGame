using UnityEngine;

public class EnemyActionHandler : MonoBehaviour
{
    private EnemyActor _enemyActor;
    private BTBranch _root;

    private void Awake()
    {
        _enemyActor = GetComponent<EnemyActor>();

        _root = new BTBranch(BranchType.BOTH);

        BTBranch battle = new BTBranch(BranchType.AND);
        battle.AddChild(new BTA_E_CheckBattle(_enemyActor));
        battle.AddChild(new BTA_E_CloseAttack(_enemyActor));

        _root.AddChild(battle);
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
