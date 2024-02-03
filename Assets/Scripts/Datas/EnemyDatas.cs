using System.Collections.Generic;

public class EnemyDatas
{
    private Dictionary<int, EnemyData> _enemyData = new Dictionary<int, EnemyData>();
    public Dictionary<int, EnemyData> EnemyData { get { return _enemyData; } set { _enemyData = value; } }

    public EnemyData GetEnemyData(int id)
    {
        if (_enemyData.ContainsKey(id) == false)
            return null;

        return _enemyData[id].Clone();
    }

    public void AddEnemyTable(ActorData actorData, RewardData rewardData)
    {
        if (_enemyData.ContainsKey(actorData.Id))
            return;

        var enemyActor = new EnemyData(actorData, rewardData);
        _enemyData.Add(actorData.Id, enemyActor);
    }
}
