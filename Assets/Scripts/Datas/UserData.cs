using System.Collections.Generic;

public class UserData
{
    private ActorData _actorData;
    public ActorData ActorData { get { return _actorData; } set { _actorData = value; } }
    private PlayerData _playerData;
    public PlayerData PlayerData { get { return _playerData; } set { _playerData = value; } }
}
