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
            playerActorData.Id = G.V.ActorId + 0;
            playerActorData.Level = 1;
            playerActorData.Hp = 10;
            playerActorData.MoveSpeed = 2;
            playerActorData.AttackSpeed = 10;
            playerActorData.AttackDamage = 2;
            var playerActorDataToJson = JsonUtility.ToJson(playerActorData);
            File.WriteAllText(playerActorDataPath, playerActorDataToJson);
        }
        var playerActorDataFromJson = File.ReadAllText(playerActorDataPath);
        playerActorData = JsonUtility.FromJson<ActorData>(playerActorDataFromJson);
        G.Data.User.ActorData = playerActorData;

        var itemDataPath = G.V.DataPath + "ItemData";
        if (File.Exists(itemDataPath) == false)
        {
            var itemDataList = new ItemDataList();
            for (int i = 0; i < 9; i++)
            {
                var index = i;
                var nowItemData = new ItemData();
                nowItemData.Id = G.V.ItemId + index;
                nowItemData.Name = $"Item{index}";
                nowItemData.Level = 0;
                nowItemData.Type = (index % 3);
                nowItemData.Value = (index / 3) + 1;
                itemDataList.Add(nowItemData);
            }
            var itemDataToJson = JsonUtility.ToJson(itemDataList);
            File.WriteAllText(itemDataPath, itemDataToJson);
        }
        var itemDataFromJson = File.ReadAllText(itemDataPath);
        var itemDatas = JsonUtility.FromJson<ItemDataList>(itemDataFromJson);
        foreach (var itemData in itemDatas.Data)
        {
            G.Data.Item.AddEquipmentData(itemData);
        }

        var skillDataPath = G.V.DataPath + "SkillData";
        if (File.Exists(skillDataPath) == false)
        {
            var skillDataList = new SkillDataList();
            for (int i = 0; i < 4; i++)
            {
                var index = i;
                var nowSkillData = new SkillData();
                nowSkillData.Id = G.V.SkillId + index;
                nowSkillData.Name = $"Skill{index}";
                nowSkillData.Level = 0;
                nowSkillData.Description = $"Skill{index} Description";
                skillDataList.Add(nowSkillData);
            }
            var skillDataToJson = JsonUtility.ToJson(skillDataList);
            File.WriteAllText(skillDataPath, skillDataToJson);
        }
        var skillataFromJson = File.ReadAllText(skillDataPath);
        var skillDatas = JsonUtility.FromJson<SkillDataList>(skillataFromJson);
        foreach (var skillData in skillDatas.Data)
        {
            G.Data.Skill.AddSkillData(skillData);
        }

        var rewardDataPath = G.V.DataPath + "RewardData";
        if (File.Exists(rewardDataPath) == false)
        {
            var rewardDataList = new RewardDataList();
            for (int i = 0; i < 5; i++)
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
                nowEnemyActorData.Id = G.V.ActorId + index;
                nowEnemyActorData.Name = $"Enemy{index}";
                nowEnemyActorData.Level = 1;
                nowEnemyActorData.Hp = 10;
                nowEnemyActorData.MoveSpeed = 1;
                nowEnemyActorData.AttackSpeed = 5;
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
            reward.AddReawrd(G.Data.Reward.GetReward(enemyActorData.Id % 5));
            G.Data.Enemy.AddEnemyTable(enemyActorData, reward);
        }

        var productDataPath = G.V.DataPath + "ProductData";
        if (File.Exists(productDataPath) == false)
        {
            var productId = 0;
            var productDataList = new ProductDataList();
            for (int i = 1; i < 5; i++)
            {
                var thisId = productId;
                ProductData newProductData = new ProductData(
                    thisId, ProductType.Currency, i, i * 10, $"{(CurrencyType)i}", $"{i * 10}", (int)CurrencyType.Gold, i * 10);
                productDataList.Add(newProductData);
                productId++;
            }

            foreach(var (id, skill) in G.Data.Skill.Skills)
            {
                var thisId = productId;
                ProductData newProductData = new ProductData(
                    thisId, ProductType.Skill, id, 1, $"{skill.Name}", $"{skill.Description}", (int)CurrencyType.Gold, 20);
                productDataList.Add(newProductData);
                productId++;
            }

            foreach (var (id, item) in G.Data.Item.Items)
            {
                var thisId = productId;
                ProductData newProductData = new ProductData(
                    thisId, ProductType.Item, id, 1, $"{item.Name}", $"{(ItemType)item.Type}", (int)CurrencyType.Gold, 20);
                productDataList.Add(newProductData);
                productId++;
            }
            var productDataToJson = JsonUtility.ToJson(productDataList);
            File.WriteAllText(productDataPath, productDataToJson);
        }
        var productDataFromJson = File.ReadAllText(productDataPath);
        var productDatas = JsonUtility.FromJson<ProductDataList>(productDataFromJson);
        foreach (var productdData in productDatas.Data)
        {
            G.Data.Product.AddProductTable(productdData);
        }
    }
}
