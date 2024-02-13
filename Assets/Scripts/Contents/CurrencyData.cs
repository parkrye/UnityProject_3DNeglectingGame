using System;
public enum CurrencyType
{
    Gold = 0,
    Diamond,
    Ruby,
    Exp,
    Emerald,
}

[Serializable]
public class CurrencyData
{
    public int Id;
    public int Count;
    public CurrencyType Type { get { return (CurrencyType)Id; } }

    public CurrencyData(CurrencyType type, int count)
    {
        Id = (int)type;
        Count = count;
    }

    public CurrencyData Clone()
    {
        var clone = new CurrencyData((CurrencyType)Id, Count);
        return clone;
    }
}