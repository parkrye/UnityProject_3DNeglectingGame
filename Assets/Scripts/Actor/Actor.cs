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

public enum ActorState
{
    Ready,
    Alive,
    Dead,
}

public class Actor : MonoBehaviour
{
    protected ActorType type;
    protected ActorState state;
}
