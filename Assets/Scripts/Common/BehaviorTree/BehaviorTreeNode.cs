using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 行为树节点
/// </summary>
public class BehaviorTreeNode
{
    private List<BehaviorTreeNode> children = new List<BehaviorTreeNode>();
    private BehaviorTreeNode parent;
    public int ChildrenLength
    {
        get
        {
            if (null == children) return 0;
            return children.Count;
        }
    }
    public bool IsIndexValid(int index)
    {
        return index >= 0 && index < ChildrenLength;
    }
    public T GetChild<T>(int index) where T : BehaviorTreeNode
    {
        if (!IsIndexValid(index)) return null;
        return (T)children[index];
    }

    public BehaviorTreeNode AddChild(BehaviorTreeNode node)
    {
        children.Add(node);
        //node._parent = this;
        return this;
    }
}
