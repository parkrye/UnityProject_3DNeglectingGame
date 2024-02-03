using System;
using System.Collections.Generic;

[Serializable]
public class RewardData
{
    public int Id;
    public List<CurrencyData> Rewards = new List<CurrencyData>();

    public void AddReawrd(CurrencyData data)
    {
        Rewards.Add(data);
    }

    public void AddReawrd(RewardData data)
    {
        foreach(var item in data.Rewards)
        {
            Rewards.Add(item);
        }
    }
}

[Serializable]
public class RewardDataList
{
    public List<RewardData> Data = new List<RewardData>();

    public void Add(RewardData data)
    {
        Data.Add(data);
    }
}
