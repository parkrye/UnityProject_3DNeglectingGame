using System.Collections.Generic;

public class Datas
{
    private UserData _userData = new UserData();
    public UserData UserData { get { return _userData; } }
    private Dictionary<string, ActorData> _enemyData = new Dictionary<string, ActorData>();
    public Dictionary<string, ActorData> EnemyData { get { return _enemyData;} set { _enemyData = value; } }
    
    public ActorData GetEnemyData(string name)
    {
        if (_enemyData.ContainsKey(name) == false)
            return null;

        return _enemyData[name];
    }
}
