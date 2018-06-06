using System;
using System.Collections.Generic;

namespace TsiU
{
    public class TBTTreeNode
    {
        //-------------------------------------------------------------------
        private const int defaultChildCount = -1; //TJQ： unlimited count
        //-------------------------------------------------------------------
        private List<TBTTreeNode> _children;
        private int _maxChildCount;
        //private TBTTreeNode _parent;
        //-------------------------------------------------------------------
        public TBTTreeNode(int maxChildCount = -1)
        {
            _children = new List<TBTTreeNode>();
            if (maxChildCount >= 0) {
                _children.Capacity = maxChildCount;
            }
            _maxChildCount = maxChildCount;
        }
        public TBTTreeNode()
            : this(defaultChildCount)
        {}
        ~TBTTreeNode()
        {
            _children = null;
            //_parent = null;
        }
        //-------------------------------------------------------------------
        public TBTTreeNode AddChild(TBTTreeNode node)
        {
            if (_maxChildCount >= 0 && _children.Count >= _maxChildCount) {
                TLogger.WARNING("**BT** exceeding child count");
                return this;
            }
            _children.Add(node);
            //node._parent = this;
            return this;
        }
        public int GetChildCount()
        {
            return _children.Count;
        }
        public bool IsIndexValid(int index)
        {
            return index >= 0 && index < _children.Count;
        }
        public T GetChild<T>(int index) where T : TBTTreeNode 
        {
            if (index < 0 || index >= _children.Count) {
                return null;
            }
            return (T)_children[index];
        }
    }
}
