using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public List<CurrencyData> Currency = new List<CurrencyData>();

    public EquipmentData Weapon;
    public EquipmentData Armor;
    public EquipmentData Accessory;

    public void AddCurrency(CurrencyData data)
    {
        Currency.Add(data);
    }
}