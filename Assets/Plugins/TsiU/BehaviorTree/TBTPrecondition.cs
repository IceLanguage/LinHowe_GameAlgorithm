using System;

namespace TsiU
{
    //---------------------------------------------------------------
    public abstract class TBTPrecondition : TBTTreeNode
    {
        public TBTPrecondition(int maxChildCount)
            : base(maxChildCount)
        {}
        public abstract bool IsTrue( /*in*/ TBTWorkingData wData);
    }
    public abstract class TBTPreconditionLeaf : TBTPrecondition
    {
        public TBTPreconditionLeaf()
            : base(0)
        {}
    }
    public abstract class TBTPreconditionUnary : TBTPrecondition
    {
        public TBTPreconditionUnary(TBTPrecondition lhs)
            : base(1)
        {
            AddChild(lhs);
        }
    }
    public abstract class TBTPreconditionBinary : TBTPrecondition
    {
        public TBTPreconditionBinary(TBTPrecondition lhs, TBTPrecondition rhs)
            : base(2)
        {
            AddChild(lhs).AddChild(rhs);
        }
    }
    //--------------------------------------------------------------
    //basic precondition
    public class TBTPreconditionTRUE : TBTPreconditionLeaf
    {
        public override bool IsTrue( /*in*/ TBTWorkingData wData)
        {
            return true;
        }
    }
    public class TBTPreconditionFALSE : TBTPreconditionLeaf
    {
        public override bool IsTrue( /*in*/ TBTWorkingData wData)
        {
            return false;
        }
    }
    //---------------------------------------------------------------
    //unary precondition
    public class TBTPreconditionNOT : TBTPreconditionUnary
    {
        public TBTPreconditionNOT(TBTPrecondition lhs)
            : base(lhs)
        {}
        public override bool IsTrue( /*in*/ TBTWorkingData wData)
        {
            return !GetChild<TBTPrecondition>(0).IsTrue(wData);
        }
    }
    //---------------------------------------------------------------
    //binary precondition
    public class TBTPreconditionAND : TBTPreconditionBinary
    {
        public TBTPreconditionAND(TBTPrecondition lhs, TBTPrecondition rhs)
            : base(lhs, rhs)
        { }
        public override bool IsTrue( /*in*/ TBTWorkingData wData)
        {
            return GetChild<TBTPrecondition>(0).IsTrue(wData) &&
                   GetChild<TBTPrecondition>(1).IsTrue(wData);
        }
    }
    public class TBTPreconditionOR : TBTPreconditionBinary
    {
        public TBTPreconditionOR(TBTPrecondition lhs, TBTPrecondition rhs)
            : base(lhs, rhs)
        { }
        public override bool IsTrue( /*in*/ TBTWorkingData wData)
        {
            return GetChild<TBTPrecondition>(0).IsTrue(wData) ||
                   GetChild<TBTPrecondition>(1).IsTrue(wData);
        }
    }
    public class TBTPreconditionXOR : TBTPreconditionBinary
    {
        public TBTPreconditionXOR(TBTPrecondition lhs, TBTPrecondition rhs)
            : base(lhs, rhs)
        { }
        public override bool IsTrue( /*in*/ TBTWorkingData wData)
        {
            return GetChild<TBTPrecondition>(0).IsTrue(wData) ^
                   GetChild<TBTPrecondition>(1).IsTrue(wData);
        }
    }
}
