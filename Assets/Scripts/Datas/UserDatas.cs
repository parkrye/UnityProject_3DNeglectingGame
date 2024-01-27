using UnityEngine;
using System.IO;

public class UserDatas
{
    private ActorData _actorData;
    public ActorData ActorData { get { return _actorData; } set { _actorData = value; } }
    private PlayerData _playerData;
    public PlayerData PlayerData { get { return _playerData; } set { _playerData = value; } }

    public int GetCurrency(CurrencyType currencyType)
    {
        foreach(var currencyData in _playerData.Currency)
        {
            if(currencyData.Id == (int)currencyType)
            {
                return currencyData.Count;
            }
        }
        return 0;
    }

    public void AddCurrency(CurrencyType currencyType, int count)
    {
        foreach (var currencyData in _playerData.Currency)
        {
            if (currencyData.Id == (int)currencyType)
            {
                currencyData.Count += count;
            }
        }
    }

    public void SetCurrency(CurrencyType currencyType, int count)
    {
        foreach (var currencyData in _playerData.Currency)
        {
            if (currencyData.Id == (int)currencyType)
            {
                currencyData.Count = count;
            }
        }
    }

    public void SaveData()
    {
        var playerDataPath = "Assets/Contents/Datas/PlayerData";
        string playerDataToJson = JsonUtility.ToJson(_playerData);
        File.WriteAllText(playerDataPath, playerDataToJson);

        var playerActorDataPath = "Assets/Contents/Datas/PlayerActorData";
        string playerActorDataToJson = JsonUtility.ToJson(_actorData);
        File.WriteAllText(playerActorDataPath, playerActorDataToJson);
    }
}