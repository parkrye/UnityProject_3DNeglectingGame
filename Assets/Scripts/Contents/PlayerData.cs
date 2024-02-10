using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public List<CurrencyData> Currency = new List<CurrencyData>();

    public ItemData Weapon = new ItemData();
    public ItemData Armor = new ItemData();
    public ItemData Accessory = new ItemData();

    public SkillData Skill1 = new SkillData();
    public SkillData Skill2 = new SkillData();
    public SkillData Skill3 = new SkillData();

    public void AddCurrency(CurrencyData data)
    {
        Currency.Add(data);
    }
}