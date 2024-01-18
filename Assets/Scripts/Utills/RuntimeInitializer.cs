using System.IO;
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
        else
        {
            var playerActorDataFromJson = File.ReadAllText(playerActorDataPath);
            playerActorData = JsonUtility.FromJson<ActorData>(playerActorDataFromJson);
        }

        Global.Datas.UserData.ActorData = playerActorData;

        for(int i = 1; i < 10; i++)
        {
            var enemyActorDataPath = $"Assets/Contents/Datas/EnemyActor{i}Data";
            ActorData enemyActorData = new ActorData();
            if (File.Exists(enemyActorDataPath) == false)
            {
                enemyActorData.Id = i;
                enemyActorData.Level = 1;
                enemyActorData.Hp = 10;
                enemyActorData.MoveSpeed = 1;
                enemyActorData.AttackSpeed = 1;
                enemyActorData.AttackDamage = 1;
                string enemyDataToJson = JsonUtility.ToJson(enemyActorData);
                File.WriteAllText(enemyActorDataPath, enemyDataToJson);
            }
            Global.Datas.EnemyData.Add($"Enemy{i}", enemyActorData);
        }
    }
}
