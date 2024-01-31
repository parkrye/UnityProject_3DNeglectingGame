using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public List<CurrencyData> Currency = new List<CurrencyData>();

    public void AddCurrency(CurrencyData data)
    {
        Currency.Add(data);
    }
}