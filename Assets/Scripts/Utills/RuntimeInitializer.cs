using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class RuntimeInitializer
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        var playerActorDataPath = "Assets/Contents/Datas/PlayerActorData";
        ActorData playerActorData = new ActorData();
        if (File.Exists(playerActorDataPath) == false)
        {
            playerActorData.Id = 0;
            playerActorData.Level = 1;
            playerActorData.Hp = 10;
            playerActorData.MoveSpeed = 1;
            playerActorData.AttackSpeed = 1;
            playerActorData.AttackDamage = 1;
            string playerDataToJson = JsonUtility.ToJson(playerActorData);
            File.WriteAllText(playerActorDataPath, playerDataToJson);
        }
        var playerActorDataFromJson = File.ReadAllText(playerActorDataPath);
        playerActorData = JsonUtility.FromJson<ActorData>(playerActorDataFromJson);
        Global.Datas.UserData.ActorData = playerActorData;

        var enemyActorDataPath = "Assets/Contents/Datas/EnemyActorData";
        if (File.Exists(enemyActorDataPath) == false)
        {
            ActorDataList enemyActorDataList = new ActorDataList();
            for (int i = 1; i < 10; i++)
            {
                ActorData nowEnemyActorData = new ActorData();
                nowEnemyActorData.Id = i;
                nowEnemyActorData.Name = $"Enemy{i}";
                nowEnemyActorData.Level = 1;
                nowEnemyActorData.Hp = 10;
                nowEnemyActorData.MoveSpeed = 1;
                nowEnemyActorData.AttackSpeed = 1;
                nowEnemyActorData.AttackDamage = 1;
                enemyActorDataList.Add(nowEnemyActorData);
            }
            var enemyDataToJson = JsonUtility.ToJson(enemyActorDataList);
            File.WriteAllText(enemyActorDataPath, enemyDataToJson);
        }
        var enemyActorDataFromJson = File.ReadAllText(enemyActorDataPath);
        var enemyActorDatas = JsonUtility.FromJson<ActorDataList>(enemyActorDataFromJson);
        foreach(var enemyActorData in enemyActorDatas.Data)
        {
            Global.Datas.EnemyData.Add(enemyActorData.Id, enemyActorData);
        }
    }
}
