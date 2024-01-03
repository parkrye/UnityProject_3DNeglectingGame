using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActionHandler : MonoBehaviour
{
    private BTBranch _root;

    private void Awake()
    {
        _root = new BTBranch(BranchType.BOTH);
    }

    public void Work()
    {
        _root.Work();
    }
}
