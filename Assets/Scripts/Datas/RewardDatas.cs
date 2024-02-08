using System.Collections.Generic;

public class RewardDatas
{
    private Dictionary<int, RewardData> _rewards = new Dictionary<int, RewardData>();
    public Dictionary<int, RewardData> Rewards { get { return _rewards; } }

    public void AddRewardTable(RewardData rewardData)
    {
        _rewards[rewardData.Id] = rewardData;
    }

    public RewardData GetReward(int id)
    {
        if(_rewards.ContainsKey(id))
            return _rewards[id];

        return null;
    }
}