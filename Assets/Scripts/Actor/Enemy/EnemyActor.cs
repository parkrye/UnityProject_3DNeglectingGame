using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActor : Actor
{
    private void Awake()
    {
        type = ActorType.Enemy;
    }
}
