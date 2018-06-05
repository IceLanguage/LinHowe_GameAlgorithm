using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class BehaviorTreeAction:BehaviorTreeNode
{
    protected static int uniqueKey = 0;
    /// <summary>
    /// 获取唯一的key值(每次调用,key加一)
    /// </summary>
    public static int GetUniqueKey()
    {
        if (uniqueKey >= int.MaxValue) return uniqueKey = 0;
        return ++uniqueKey;

    }
    public string Name { get; set; }

    protected BehaviorTreePrecondition precondition;

    public BehaviorTreeAction()
    {
        uniqueKey = BehaviorTreeAction.GetUniqueKey();
    }

    public override int GetHashCode()
    {
        return uniqueKey;
    }

    public bool Evaluate()
    {
        return (precondition == null || precondition.IsTrue()) && onEvaluate();
    }
    public void Transition()
    {
        onTransition();
    }
    public BehaviorTreeAction SetPrecondition(BehaviorTreePrecondition precondition)
    {
        this.precondition = precondition;
        return this;
    }

    //--------------------------------------------------------
    // inherented by children
    protected virtual bool onEvaluate()
    {
        return true;
    }
    protected virtual void onTransition()
    {
    }
}
