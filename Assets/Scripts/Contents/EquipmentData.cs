using System;
using System.Collections.Generic;

public enum EquipmentType
{
    Weapon,
    Armor,
    Accessory,
}

[Serializable]
public class EquipmentData
{
    public int Id;
    public string Name;
    public int Level;
    public int Type;
    public int Value;

    public EquipmentType GetEquipmentType()
    {
        return (EquipmentType)Type;
    }

    public EquipmentData Clone()
    {
        EquipmentData clone = new EquipmentData();
        clone.Id = Id;
        clone.Name = Name;
        clone.Level = Level;
        clone.Type = Type;
        clone.Value = Value;
        return clone;
    }
}

[Serializable]
public class EquipmentDataList
{
    public List<EquipmentData> Data = new List<EquipmentData>();

    public void Add(EquipmentData data)
    {
        Data.Add(data);
    }
}
