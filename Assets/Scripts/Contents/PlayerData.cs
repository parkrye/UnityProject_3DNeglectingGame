using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine.Events;

[Serializable]
public class PlayerData
{
    public List<CurrencyData> Currency = new List<CurrencyData>();

    public ItemData Weapon = new ItemData();
    public ItemData Armor = new ItemData();
    public ItemData Accessory = new ItemData();

    public SkillData Skill1 { get; private set; }
    public SkillData Skill2 { get; private set; }
    public SkillData Skill3 { get; private set; }

    [JsonIgnore]
    public UnityEvent<int, SkillData, SkillData> SkillChangeEvent = new UnityEvent<int, SkillData, SkillData>();

    public void AddCurrency(CurrencyData data)
    {
        Currency.Add(data);
    }

    public void SetSkill(int slot, SkillData before, SkillData after)
    {
        switch (slot)
        {
            case 0:
                Skill1 = after;
                if (Skill2 != null && Skill2.Id == after.Id)
                {
                    Skill2 = null;
                    SkillChangeEvent.Invoke(1, after, null);
                }
                if (Skill3 != null && Skill3.Id == after.Id)
                {
                    Skill3 = null;
                    SkillChangeEvent.Invoke(2, after, null);
                }
                break;
            case 1:
                Skill2 = after;
                if (Skill1 != null && Skill1.Id == after.Id)
                {
                    Skill1 = null;
                    SkillChangeEvent.Invoke(0, after, null);
                }
                if (Skill3 != null && Skill3.Id == after.Id)
                {
                    Skill3 = null;
                    SkillChangeEvent.Invoke(2, after, null);
                }
                break;
            case 2:
                Skill3 = after;
                if (Skill1 != null && Skill1.Id == after.Id)
                {
                    Skill1 = null;
                    SkillChangeEvent.Invoke(0, after, null);
                }
                if (Skill2 != null && Skill2.Id == after.Id)
                {
                    Skill2 = null;
                    SkillChangeEvent.Invoke(1, after, null);
                }
                break;
        }
        SkillChangeEvent.Invoke(slot, before, after);
    }
}