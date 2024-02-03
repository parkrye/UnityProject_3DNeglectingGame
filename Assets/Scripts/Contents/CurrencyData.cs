using System;
public enum CurrencyType
{
    Gold = 0,
    Diamond,
    Ruby,
    Exp,
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

    public CurrencyData Clone()
    {
        var clone = new CurrencyData((CurrencyType)Id, Count);
        return clone;
    }
}