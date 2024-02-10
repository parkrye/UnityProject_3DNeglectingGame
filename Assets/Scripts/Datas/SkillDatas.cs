using System.Collections.Generic;

public class SkillDatas
{

    private Dictionary<int, SkillData> _skills = new Dictionary<int, SkillData>();
    public Dictionary<int, SkillData> Skills { get { return _skills; } set { _skills = value; } }

    public void AddSkillData(SkillData skillData)
    {
        _skills[skillData.Id] = skillData;
    }

    public SkillData GetSkillData(int id)
    {
        if (_skills.ContainsKey(id) == false)
            return null;

        return _skills[id].Clone();
    }
}
