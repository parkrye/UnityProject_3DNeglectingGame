using System;
using System.Collections.Generic;

public enum ItemType
{
    Weapon,
    Armor,
    Accessory,
}

[Serializable]
public class ItemData
{
    public int Id;
    public string Name;
    public int Level;
    public int Type;
    public int Value;

    public ItemType GetItemType()
    {
        return (ItemType)Type;
    }

    public ItemData Clone()
    {
        ItemData clone = new ItemData();
        clone.Id = Id;
        clone.Name = Name;
        clone.Level = Level;
        clone.Type = Type;
        clone.Value = Value;
        return clone;
    }
}

[Serializable]
public class ItemDataList
{
    public List<ItemData> Data = new List<ItemData>();

    public void Add(ItemData data)
    {
        Data.Add(data);
    }
}
