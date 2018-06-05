using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class BehaviorTree
{
    private BehaviorTree[] children;
    private BehaviorTree parent;
    public int ChildrenLength
    {
        get
        {
            return children.Length;
        }
    }
    public BehaviorTree(int maxChildCount = 16)
    {        
        if (maxChildCount > 0)
        {
            children = new BehaviorTree[maxChildCount];
        }
    }
    public bool IsIndexValid(int index)
    {
        return index >= 0 && index < ChildrenLength;
    }
    public T GetChild<T>(int index) where T : BehaviorTree
    {
        if (!IsIndexValid(index)) return null;
        return (T)children[index];
    }
}
