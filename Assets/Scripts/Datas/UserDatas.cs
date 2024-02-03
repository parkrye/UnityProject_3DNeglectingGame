using UnityEngine;
using System.IO;
using UnityEngine.Events;

public class UserDatas
{
    private ActorData _actorData;
    public ActorData ActorData { get { return _actorData; } set { _actorData = value; } }
    private PlayerData _playerData;
    public PlayerData PlayerData { get { return _playerData; } set { _playerData = value; } }
    public UnityEvent<CurrencyData> CurrencyUpdate = new UnityEvent<CurrencyData>();

    public int GetCurrency(CurrencyType currencyType)
    {
        foreach(var currencyData in _playerData.Currency)
        {
            if(currencyData.Type == currencyType)
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
            if (currencyData.Type == currencyType)
            {
                currencyData.Count += count;
                CurrencyUpdate?.Invoke(currencyData);
            }
        }
    }

    public void SetCurrency(CurrencyType currencyType, int count)
    {
        foreach (var currencyData in _playerData.Currency)
        {
            if (currencyData.Type == currencyType)
            {
                currencyData.Count = count;
                CurrencyUpdate?.Invoke(currencyData);
            }
        }
    }

    public bool TryUseCurrency(CurrencyType currencyType, int count)
    {
        foreach (var currencyData in _playerData.Currency)
        {
            if (currencyData.Type == currencyType)
            {
                if(currencyData.Count >= count)
                {
                    currencyData.Count -= count;
                    CurrencyUpdate?.Invoke(currencyData);
                    return true;
                }
                return false;
            }
        }

        return false;
    }

    public void SaveData()
    {
        var playerDataPath = G.V.DataPath + "PlayerData";
        string playerDataToJson = JsonUtility.ToJson(_playerData);
        File.WriteAllText(playerDataPath, playerDataToJson);

        var playerActorDataPath = G.V.DataPath + "PlayerActorData";
        string playerActorDataToJson = JsonUtility.ToJson(_actorData);
        File.WriteAllText(playerActorDataPath, playerActorDataToJson);
    }
}
