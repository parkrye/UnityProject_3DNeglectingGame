using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public List<CurrencyData> Data = new List<CurrencyData>();

    public void Add(CurrencyData data)
    {
        Data.Add(data);
    }
}

[Serializable]
public class CurrencyData
{
    public int Id;
    public int Count;

    public CurrencyData(CurrencyType type, int count)
    {
        Id = (int)type;
        Count = count;
    }
}