public enum ActionState
{
    Ready,
    Working,
    End,
}

public class BTAction
{
    private ActionState _state;

    public virtual bool Work()
    {
        return true;
    }

    public virtual void Reset()
    {
        _state = ActionState.Ready;
    }
}
