using UnityEngine;

public class EnemyActionHandler : MonoBehaviour
{
    private BTBranch _root;
    private NormalAnimationController _anim;

    private void Awake()
    {
        _anim = GetComponent<NormalAnimationController>();
        _root = new BTBranch(BranchType.BOTH);
    }

    public void Work()
    {
        _root.Work();
    }
}
