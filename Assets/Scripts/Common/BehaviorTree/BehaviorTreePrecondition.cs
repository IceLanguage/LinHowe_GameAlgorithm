using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 行为树选择前提
/// </summary>
public abstract class BehaviorTreePrecondition:BehaviorTreeNode
{
    public abstract bool IsTrue();
}

public abstract class BehaviorTreePreconditionLeaf : BehaviorTreePrecondition
{
}
public abstract class BehaviorTreePreconditionUnary : BehaviorTreePrecondition
{
    public BehaviorTreePreconditionUnary(BehaviorTreePrecondition p)
    {
        AddChild(p);
    }
}
public abstract class BehaviorTreePreconditionBinary : BehaviorTreePrecondition
{
    public BehaviorTreePreconditionBinary(BehaviorTreePrecondition left, BehaviorTreePrecondition right)
    {
        AddChild(left).AddChild(right);
    }
}
//--------------------------------------------------------------
//basic precondition
public class BehaviorTreePreconditionTRUE : BehaviorTreePreconditionLeaf
{
    public override bool IsTrue()
    {
        return true;
    }
}
public class BehaviorTreePreconditionFALSE : BehaviorTreePreconditionLeaf
{
    public override bool IsTrue()
    {
        return false;
    }
}
//---------------------------------------------------------------
//unary precondition
public class BehaviorTreePreconditionNOT : BehaviorTreePreconditionUnary
{
    public BehaviorTreePreconditionNOT(BehaviorTreePrecondition p)
        : base(p)
    { }
    public override bool IsTrue()
    {
        return !GetChild<BehaviorTreePrecondition>(0).IsTrue();
    }
}
//---------------------------------------------------------------
//binary precondition
public class BehaviorTreePreconditionAND : BehaviorTreePreconditionBinary
{
    public BehaviorTreePreconditionAND(BehaviorTreePrecondition lhs, BehaviorTreePrecondition rhs)
        : base(lhs, rhs)
    { }
    public override bool IsTrue()
    {
        return GetChild<BehaviorTreePrecondition>(0).IsTrue() &&
               GetChild<BehaviorTreePrecondition>(1).IsTrue();
    }
}
public class BehaviorTreePreconditionOR : BehaviorTreePreconditionBinary
{
    public BehaviorTreePreconditionOR(BehaviorTreePrecondition lhs, BehaviorTreePrecondition rhs)
        : base(lhs, rhs)
    { }
    public override bool IsTrue()
    {
        return GetChild<BehaviorTreePrecondition>(0).IsTrue() ||
               GetChild<BehaviorTreePrecondition>(1).IsTrue();
    }
}
public class BehaviorTreePreconditionXOR : BehaviorTreePreconditionBinary
{
    public BehaviorTreePreconditionXOR(BehaviorTreePrecondition lhs, BehaviorTreePrecondition rhs)
        : base(lhs, rhs)
    { }
    public override bool IsTrue()
    {
        return GetChild<BehaviorTreePrecondition>(0).IsTrue() ^
               GetChild<BehaviorTreePrecondition>(1).IsTrue();
    }
}
