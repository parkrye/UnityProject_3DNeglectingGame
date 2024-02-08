using System.IO;
using UnityEngine;

public class RuntimeInitializer
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        InitData();
    }

    private static void InitData()
    {
        var playerDataPath = G.V.DataPath + "PlayerData";
        var playerData = new PlayerData();
        if (File.Exists(playerDataPath) == false)
        {
            playerData.Currency.Add(new CurrencyData(CurrencyType.Gold, 0));
            playerData.Currency.Add(new CurrencyData(CurrencyType.Diamond, 0));
            playerData.Currency.Add(new CurrencyData(CurrencyType.Ruby, 0));
            playerData.Currency.Add(new CurrencyData(CurrencyType.Exp, 0));
            var playerDataToJson = JsonUtility.ToJson(playerData);
            File.WriteAllText(playerDataPath, playerDataToJson);
        }
        var playerDataFromJson = File.ReadAllText(playerDataPath);
        playerData = JsonUtility.FromJson<PlayerData>(playerDataFromJson);
        G.Data.User.PlayerData = playerData;

        var playerActorDataPath = G.V.DataPath + "PlayerActorData";
        var playerActorData = new ActorData();
        if (File.Exists(playerActorDataPath) == false)
        {
            playerActorData.Id = 0;
            playerActorData.Level = 1;
            playerActorData.Hp = 10;
            playerActorData.MoveSpeed = 2;
            playerActorData.AttackSpeed = 1;
            playerActorData.AttackDamage = 1;
            var playerActorDataToJson = JsonUtility.ToJson(playerActorData);
            File.WriteAllText(playerActorDataPath, playerActorDataToJson);
        }
        var playerActorDataFromJson = File.ReadAllText(playerActorDataPath);
        playerActorData = JsonUtility.FromJson<ActorData>(playerActorDataFromJson);
        G.Data.User.ActorData = playerActorData;

        var rewardDataPath = G.V.DataPath + "RewardData";
        if (File.Exists(rewardDataPath) == false)
        {
            var rewardDataList = new RewardDataList();
            for (int i = 0; i < 4; i++)
            {
                var id = i;
                RewardData nowRewardData = new RewardData();
                nowRewardData.Id = id;
                nowRewardData.AddReawrd(new CurrencyData((CurrencyType)id, 10));
                rewardDataList.Add(nowRewardData);
            }
            var rewardDataToJson = JsonUtility.ToJson(rewardDataList);
            File.WriteAllText(rewardDataPath, rewardDataToJson);
        }
        var rewardDataFromJson = File.ReadAllText(rewardDataPath);
        var rewardDatas = JsonUtility.FromJson<RewardDataList>(rewardDataFromJson);
        foreach (var rewardData in rewardDatas.Data)
        {
            G.Data.Reward.AddRewardTable(rewardData);
        }

        var enemyActorDataPath = G.V.DataPath + "EnemyActorData";
        if (File.Exists(enemyActorDataPath) == false)
        {
            var enemyActorDataList = new ActorDataList();
            for (int i = 1; i < 10; i++)
            {
                var index = i;
                var nowEnemyActorData = new ActorData();
                nowEnemyActorData.Id = index;
                nowEnemyActorData.Name = $"Enemy{index}";
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
        foreach (var enemyActorData in enemyActorDatas.Data)
        {
            var reward = new RewardData();
            reward.AddReawrd(G.Data.Reward.GetReward(3));
            reward.AddReawrd(G.Data.Reward.GetReward(enemyActorData.Id % 3));
            G.Data.Enemy.AddEnemyTable(enemyActorData, reward);
        }

        var equipmentDataPath = G.V.DataPath + "EquipmentData";
        if(File.Exists(equipmentDataPath) == false)
        {
            var equipmentDataList = new EquipmentDataList();
            for(int i = 0; i < 9; i++)
            {
                var index = i;
                var nowEquipemntData = new EquipmentData();
                nowEquipemntData.Id = index;
                nowEquipemntData.Name = $"Equipment{index}";
                nowEquipemntData.Level = 0;
                nowEquipemntData.Type = (index % 3);
                nowEquipemntData.Value = (index / 3) + 1;
                equipmentDataList.Add(nowEquipemntData);
            }
            var equipmentDataToJson = JsonUtility.ToJson(equipmentDataList);
            File.WriteAllText(equipmentDataPath, equipmentDataToJson);
        }

        var equipmentDataFromJson = File.ReadAllText(equipmentDataPath);
        var equipmentDatas = JsonUtility.FromJson<EquipmentDataList>(equipmentDataFromJson);
        foreach (var equipmentData in equipmentDatas.Data)
        {
            G.Data.Equipment.AddEquipmentData(equipmentData);
        }
    }
}
