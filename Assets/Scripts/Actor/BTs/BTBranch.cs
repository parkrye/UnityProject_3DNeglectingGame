using System.Collections.Generic;

public enum BranchType
{
    BOTH,
    AND,
    OR,
}

public class BTBranch : BTAction
{
    private BranchType _branchType;
    private List<BTAction> _actions = new List<BTAction>();

    public BTBranch(BranchType branchType)
    {
        _branchType = branchType;
    }

    public override bool Work()
    {
        var prevArgs = new BTArgs();
        switch (_branchType)
        {
            case BranchType.BOTH:
                foreach (var action in _actions)
                {
                    if(prevArgs.IsNull == false)
                        action.Args = prevArgs;

                    action.Work();

                    if(action.Args.IsNull == false)
                        prevArgs = action.Args;
                }
                return true;
            case BranchType.AND:
                for(int i = 0; i < _actions.Count; i++)
                {
                    if (prevArgs.IsNull == false)
                        _actions[i].Args = prevArgs;

                    if (_actions[i].Work() == false)
                        return false;

                    if (_actions[i].Args.IsNull == false)
                        prevArgs = _actions[i].Args;
                }
                foreach (var action in _actions)
                {
                    action.Reset();
                }
                return true;
            case BranchType.OR:
                for (int i = 0; i < _actions.Count; i++)
                {
                    if (prevArgs.IsNull == false)
                        _actions[i].Args = prevArgs;

                    if (_actions[i].Work() == true)
                    {
                        for (int j = 0; j <= i; j++)
                        {
                            _actions[j].Reset();
                        }
                        return true;
                    }

                    if (_actions[i].Args.IsNull == false)
                        prevArgs = _actions[i].Args;
                }
                return false;
        }
        return false;
    }

    public void AddChild(BTAction child)
    {
        _actions.Add(child);
    }
    
    public void ResetChildren()
    {
        foreach (var action in _actions)
        {
            action.Reset();
        }
    }
}
