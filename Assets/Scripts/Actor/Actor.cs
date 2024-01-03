using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActorType
{
    PC,
    NPC,
    Enemy,
    Boss,
}

public class Actor : MonoBehaviour
{
    protected ActorType type;
}
