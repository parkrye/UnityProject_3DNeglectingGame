using System;
using System.Collections.Generic;

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
        ActorData clone = new ActorData();
        clone.Id = Id;
        clone.Name = Name;
        clone.Level = Level;
        clone.Hp = Hp;
        clone.MoveSpeed = MoveSpeed;
        clone.AttackSpeed = AttackSpeed;
        clone.AttackDamage = AttackDamage;
        return clone;
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

    public void AddReward(RewardData rewardData)
    {
        var newRewardData = new RewardData();
        foreach(var reward in _rewardData.Rewards)
        {
            newRewardData.AddReawrd(reward.Clone());
        }
        foreach(var reward in rewardData.Rewards)
        {
            newRewardData.AddReawrd(reward.Clone());
        }
        _rewardData = newRewardData;
    }
}