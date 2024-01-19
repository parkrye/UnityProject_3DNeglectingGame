using UnityEngine;

public class PlayerActionHandler : MonoBehaviour
{
    private BTBranch _root;
    private NormalAnimationController _anim;

    private void Awake()
    {
        _anim = GetComponent<NormalAnimationController>();

        _root = new BTBranch(BranchType.BOTH);

        BTBranch findAndMove = new BTBranch(BranchType.AND);
        findAndMove.AddChild(new BTA_MoveToClose(_anim));
        findAndMove.AddChild(new BTA_AttackClose(_anim));

        _root.AddChild(findAndMove);
    }

    public void Work()
    {
        _root.Work();
    }
}
