using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionHandler : MonoBehaviour
{
    private BTBranch _root;

    private void Awake()
    {
        _root = new BTBranch(BranchType.BOTH);

        BTBranch findAndMove = new BTBranch(BranchType.AND);
        findAndMove.AddChild(new BTA_MoveToClose());

        _root.AddChild(findAndMove);
    }

    public void Work()
    {
        _root.Work();
    }
}
