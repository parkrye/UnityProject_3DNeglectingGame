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

        Global.UD.ActorData = playerActorData;
    }
}
