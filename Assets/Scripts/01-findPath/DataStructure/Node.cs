using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LinHoweFindPath
{
    //节点坐标
    /// <summary>
    /// 节点
    /// </summary>
    [Serializable]
    public struct Node
    {
        public Node(int x, int z) { this.x = x; this.z = z; }
        [SerializeField]
        public int x, z;
        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            //如果引用需要判断引用是否相等
            //if (ReferenceEquals(this, obj)) return true;
            if (this.GetType() != obj.GetType()) return false;
            var o = (Node)obj;
            return (x.Equals(o.x) && (z.Equals(o.z)));

        }
        public override int GetHashCode()
        {
            int hashcode = x.GetHashCode();

            //应保证操作数不相等或相近，否则结构为0
            if (x.GetHashCode() != z.GetHashCode())
            {
                hashcode ^= z.GetHashCode();
            }
            return hashcode;
        }
        public static bool operator ==(Node A, Node B)
        {
            //if(ReferenceEquals(A, null)) return ReferenceEquals(B, null);
            return A.Equals(B);
        }
        public static bool operator !=(Node a, Node b)
        {
            return !(a == b);
        }
    }
}
