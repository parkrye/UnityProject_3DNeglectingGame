using System.Collections.Generic;

public class SkillDatas
{

    private Dictionary<int, SkillData> _skills = new Dictionary<int, SkillData>();
    public Dictionary<int, SkillData> Skills { get { return _skills; } }

    private Dictionary<int, BTAction> _actions = new Dictionary<int, BTAction>();
    public Dictionary<int, BTAction> Actions { get { return _actions; } }

    public void AddSkillData(SkillData skillData)
    {
        _skills[skillData.Id] = skillData;
        if(skillData.Id % 2 == 0)
            _actions[skillData.Id] = new BTA_CloseDoubleAttack();
        else
            _actions[skillData.Id] = new BTA_SelfHeal();
    }

    public SkillData GetSkillData(int id)
    {
        if (_skills.ContainsKey(id) == false)
            return null;

        return _skills[id].Clone();
    }

    public BTAction GetSkillAction(int id)
    {
        if (_actions.ContainsKey(id) == false)
            return null;

        return _actions[id];
    }
}
