using System;
using System.Collections.Generic;

[Serializable]
public class SkillData
{
    public int Id; 
    public string Name;
    public int Level;
    public string Description;

    public bool HasSkill()
    {
        return Level > 0;
    }

    public SkillData Clone()
    {
        var clone = new SkillData();
        clone.Id = Id;
        clone.Name = Name;
        clone.Level = Level;
        clone.Description = Description;
        return clone;
    }
}

[Serializable]
public class SkillDataList
{
    public List<SkillData> Data = new List<SkillData>();

    public void Add(SkillData data)
    {
        Data.Add(data);
    }
}