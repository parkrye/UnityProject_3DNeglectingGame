using UnityEngine;

public class PlayerActionHandler : MonoBehaviour
{
    private BTBranch _root;

    private void Awake()
    {
        _root = new BTBranch(BranchType.BOTH);

        _root.AddChild(new BTA_CloseAttack());
    }

    private void Start()
    {
        G.Data.User.PlayerData.SkillChangeEvent.AddListener(ModifySkill);
    }

    public void Work()
    {
        _root.Work();
    }

    public void ResetBT()
    {
        _root.ResetChildren();
    }

    private void ModifySkill(int _, SkillData before,  SkillData after)
    {
        if (before != null)
            _root.RemoveChildren(G.Data.Skill.GetSkillAction(before.Id));
        if (after != null)
            _root.AddChild(G.Data.Skill.GetSkillAction(after.Id));
    }
}
