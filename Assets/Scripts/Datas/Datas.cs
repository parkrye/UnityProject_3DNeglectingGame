using System.Collections.Generic;

public class Datas
{
    private UserData _userData = new UserData();
    public UserData UserData { get { return _userData; } }
    private Dictionary<int, ActorData> _enemyData = new Dictionary<int, ActorData>();
    public Dictionary<int, ActorData> EnemyData { get { return _enemyData;} set { _enemyData = value; } }
    
    public ActorData GetEnemyData(int id)
    {
        if (_enemyData.ContainsKey(id) == false)
            return null;

        return _enemyData[id];
    }
}
