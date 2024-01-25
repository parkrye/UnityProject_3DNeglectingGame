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

    public ActorData Clone()
    {
        ActorData clone = new ActorData();
        clone.Id = Id;
        clone.Name = Name;
        clone.Level = Level;
        clone.Hp = Hp;
        clone.MoveSpeed = MoveSpeed;
        clone.AttackSpeed = AttackSpeed;
        clone.AttackDamage = AttackDamage;
        return clone;
    }
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