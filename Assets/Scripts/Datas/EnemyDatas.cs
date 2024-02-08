using System.Collections.Generic;

public class EnemyDatas
{
    private Dictionary<int, EnemyData> _enemyData = new Dictionary<int, EnemyData>();
    public Dictionary<int, EnemyData> EnemyData { get { return _enemyData; } set { _enemyData = value; } }

    public void AddEnemyTable(ActorData actorData, RewardData rewardData)
    {
        _enemyData[actorData.Id] = new EnemyData(actorData, rewardData);
    }

    public EnemyData GetEnemyData(int id)
    {
        if (_enemyData.ContainsKey(id) == false)
            return null;

        return _enemyData[id].Clone();
    }
}
