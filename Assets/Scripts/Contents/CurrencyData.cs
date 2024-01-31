using System;

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