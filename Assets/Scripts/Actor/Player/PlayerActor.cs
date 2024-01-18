using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : Actor
{
    private PlayerActionHandler actionHandler;

    private void Awake()
    {
        actionHandler = GetComponent<PlayerActionHandler>();
        if(actionHandler == null)
            actionHandler = gameObject.AddComponent<PlayerActionHandler>();
        type = ActorType.PC;
        state = ActorState.Ready;
    }

    public void StageStarted()
    {
        state = ActorState.Alive;
        ActionRoutine().Forget();
    }

    private async UniTask ActionRoutine()
    {
        await UniTask.WaitUntil(() => state == ActorState.Alive);

        while (state == ActorState.Alive)
        {
            actionHandler.Work();
            await UniTask.WaitForFixedUpdate();
        }
    }
}
