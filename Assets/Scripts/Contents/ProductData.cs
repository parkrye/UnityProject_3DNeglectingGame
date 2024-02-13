using System;
using System.Collections.Generic;

[Serializable]
public class ProductData
{
    public int Id;
    public int Type;
    public int Count;
    public string Name;
    public string Descripntion;
    public int Cost;

    public ProductData(int id, CurrencyType type, int count, string name, string descripntion, int cost)
    {
        Id = id;
        Type = (int)type;
        Count = count;
        Name = name;
        Descripntion = descripntion;
        Cost = cost;
    }

    public ProductData Clone()
    {
        var clone = new ProductData(Id, (CurrencyType)Type, Count, Name, Descripntion, Cost);
        return clone;
    }
}

[Serializable]
public class ProductDataList
{
    public List<ProductData> Data = new List<ProductData>();

    public void Add(ProductData data)
    {
        Data.Add(data);
    }
}