using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public List<CurrencyData> Currency = new List<CurrencyData>();

    public ItemData Weapon;
    public ItemData Armor;
    public ItemData Accessory;

    public void AddCurrency(CurrencyData data)
    {
        Currency.Add(data);
    }
}