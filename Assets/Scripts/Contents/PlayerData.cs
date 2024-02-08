using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public List<CurrencyData> Currency = new List<CurrencyData>();

    public EquipmentData Weapon = new EquipmentData();
    public EquipmentData Armor = new EquipmentData();
    public EquipmentData Accessory = new EquipmentData();

    public void AddCurrency(CurrencyData data)
    {
        Currency.Add(data);
    }
}