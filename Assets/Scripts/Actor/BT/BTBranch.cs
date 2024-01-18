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
        switch (_branchType)
        {
            case BranchType.BOTH:
                foreach (var action in _actions)
                {
                    action.Work();
                }
                return true;
            case BranchType.AND:
                foreach (var action in _actions)
                {
                    if (action.Work() == false)
                    {
                        ResetChildren();
                        return false;
                    }
                }
                return true;
            case BranchType.OR:
                foreach (var action in _actions)
                {
                    if (action.Work() == true)
                    {
                        ResetChildren();
                        return true;
                    }
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
