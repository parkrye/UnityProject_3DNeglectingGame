using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public List<CurrencyData> Currency = new List<CurrencyData>();

    public ItemData Weapon = new ItemData();
    public ItemData Armor = new ItemData();
    public ItemData Accessory = new ItemData();

    public void AddCurrency(CurrencyData data)
    {
        Currency.Add(data);
    }
}