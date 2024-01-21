using UnityEngine;

public class EnemyActionHandler : MonoBehaviour
{
    private BTBranch _root;

    private void Awake()
    {
        _root = new BTBranch(BranchType.OR);

        BTBranch battle = new BTBranch(BranchType.AND);
        battle.AddChild(new BTA_E_CheckBattle());
        BTBranch battleAndChace = new BTBranch(BranchType.AND);
        battleAndChace.AddChild(new BTA_E_Chacing());
        battleAndChace.AddChild(new BTA_E_CloseAttack());
        battle.AddChild(battleAndChace);

        _root.AddChild(battle);
        _root.AddChild(new BTA_E_WalkAround());
    }

    public void Work()
    {
        _root.Work();
    }
}
