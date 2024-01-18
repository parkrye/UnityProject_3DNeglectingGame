using System.Collections.Generic;

public class UserData
{
    private Dictionary<CurrencyType, double> _currency = new Dictionary<CurrencyType, double>();
    public Dictionary<CurrencyType, double> Currency { get { return _currency; } }
    private ActorData _actorData;
    public ActorData ActorData { get { return _actorData; } set { _actorData = value; } }
}
