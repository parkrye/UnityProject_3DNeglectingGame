using System;
using System.Collections.Generic;

public enum ProductType
{
    Currency = 0,
    Skill,
    Item,
}

[Serializable]
public class ProductData
{
    public int Id;
    public int Type;
    public int TargetId;
    public int Count;
    public string Name;
    public string Descripntion;
    public int CostId;
    public int Cost;

    public ProductType ProductType { get { return (ProductType)Type; } }
    public CurrencyType CostType { get { return (CurrencyType)CostId; } }

    public ProductData(int id, ProductType type, int targetId, int count, string name, string descripntion, int costId, int cost)
    {
        Id = id;
        Type = (int)type;
        TargetId = targetId;
        Count = count;
        Name = name;
        Descripntion = descripntion;
        CostId = costId;
        Cost = cost;
    }

    public ProductData Clone()
    {
        var clone = new ProductData(Id, (ProductType)Type, TargetId, Count, Name, Descripntion, CostId, Cost);
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