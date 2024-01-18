public enum ActionState
{
    Ready,
    Working,
    End,
}

public abstract class BTAction
{
    protected ActionState _state;

    public virtual bool Work()
    {
        switch (_state)
        {
            case ActionState.Ready:
                break;
            case ActionState.Working:
                break;
            case ActionState.End:
                break;
        }

        return true;
    }

    public virtual void Reset()
    {
        _state = ActionState.Ready;
    }
}
