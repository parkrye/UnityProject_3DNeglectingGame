using System;
using System.Collections.Generic;
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

    public UnityEvent<int, SkillData, SkillData> SkillChangeEvent = new UnityEvent<int, SkillData, SkillData>();

    public void AddCurrency(CurrencyData data)
    {
        Currency.Add(data);
    }

    public void SetSkill(int slot, SkillData before, SkillData after)
    {
        switch(slot)
        {
            case 0:
                Skill1 = after;
                break;
            case 1:
                Skill2 = after;
                break;
            case 2:
                Skill3 = after;
                break;
        }
        SkillChangeEvent.Invoke(slot, before, after);
    }
}