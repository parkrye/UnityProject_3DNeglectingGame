using System;
using System.Collections.Generic;

public enum Status
{
    Level = 0,
    Hp,
    MoveSpeed,
    AttackSpeed,
    AttackDamage
}

[Serializable]
public class ActorData
{
    public int Id;
    public string Name;
    public int Level;
    public int Hp;
    public int MoveSpeed;
    public int AttackSpeed;
    public int AttackDamage;

    public ActorData Clone()
    {
        var clone = new ActorData();
        clone.Id = Id;
        clone.Name = Name;
        clone.Level = Level;
        clone.Hp = Hp;
        clone.MoveSpeed = MoveSpeed;
        clone.AttackSpeed = AttackSpeed;
        clone.AttackDamage = AttackDamage;
        return clone;
    }

    public bool IsCorrect()
    {
        if (Id < G.V.ActorId || Id >= G.V.ItemId)
            return false;
        return true;
    }

    public float GetLevelUpCost(Status target)
    {
        switch(target)
        {
            case Status.Level:
                return G.Data.User.GetCurrency(CurrencyType.Exp) / Level;
            case Status.Hp:
                return Hp;
            case Status.MoveSpeed:
                return MoveSpeed;
            case Status.AttackSpeed:
                return AttackSpeed;
            case Status.AttackDamage:
                return AttackDamage;
        }
        return -1;
    }

    public bool TryLevelUp(Status target)
    {
        switch (target)
        {
            case Status.Level:
                if (G.Data.User.TryUseCurrency(CurrencyType.Exp, Level))
                {
                    Level++;
                    return true;
                }
                break;
            case Status.Hp:
                if (G.Data.User.TryUseCurrency(CurrencyType.Diamond, Hp))
                {
                    Hp++;
                    return true;
                }
                break;
            case Status.MoveSpeed:
                if (G.Data.User.TryUseCurrency(CurrencyType.Diamond, MoveSpeed))
                {
                    MoveSpeed++;
                    return true;
                }
                break;
            case Status.AttackSpeed:
                if (G.Data.User.TryUseCurrency(CurrencyType.Diamond, AttackSpeed))
                {
                    AttackSpeed++;
                    return true;
                }
                break;
            case Status.AttackDamage:
                if (G.Data.User.TryUseCurrency(CurrencyType.Diamond, AttackDamage))
                {
                    AttackDamage++;
                    return true;
                }
                break;
        }
        return false;
    }
}

[Serializable]
public class ActorDataList
{
    public List<ActorData> Data = new List<ActorData>();

    public void Add(ActorData data)
    {
        Data.Add(data);
    }
}

public class EnemyData
{
    private ActorData _enemyActorData;
    public ActorData EnemyActorData { get { return _enemyActorData; } }
    private RewardData _rewardData;
    public RewardData RewardData { get { return _rewardData; } }

    public EnemyData(ActorData enemyActorData, RewardData rewardData)
    {
        _enemyActorData = enemyActorData;
        _rewardData = rewardData;
    }

    public EnemyData Clone()
    {
        return new EnemyData(_enemyActorData.Clone(), _rewardData);
    }
}