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
                for(int i = 0; i < _actions.Count; i++)
                {
                    if (_actions[i].Work() == false)
                    {
                        return false;
                    }
                }
                foreach (var action in _actions)
                {
                    action.Reset();
                }
                return true;
            case BranchType.OR:
                for (int i = 0; i < _actions.Count; i++)
                {
                    if (_actions[i].Work() == true)
                    {
                        for (int j = 0; j <= i; j++)
                        {
                            _actions[j].Reset();
                        }
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
