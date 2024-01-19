using System;
using System.Collections.Generic;

[Serializable]
public class ActorData
{
    public int Id;
    public string Name;
    public int Level;
    public int Hp;
    public int MoveSpeed;
    public int AttackSpeed;
    public int AttackDamage;
}

[Serializable]
public class ActorDataList
{
    public List<ActorData> Data = new List<ActorData>();

    public void Add(ActorData data)
    {
        Data.Add(data);
    }
}