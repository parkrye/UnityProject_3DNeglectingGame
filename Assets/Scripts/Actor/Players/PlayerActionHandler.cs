using UnityEngine;

public class PlayerActionHandler : MonoBehaviour
{
    private BTBranch _root;

    private void Awake()
    {
        _root = new BTBranch(BranchType.BOTH);

        BTBranch battleBranch = new BTBranch(BranchType.BOTH);
        battleBranch.AddChild(new BTA_CloseAttack());

        _root.AddChild(battleBranch);
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
