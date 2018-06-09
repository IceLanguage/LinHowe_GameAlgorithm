using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CollisionDetection
{

    /// <summary>
    /// 八叉树节点
    /// </summary>
    public class OctTreeNode<T>
        where T : struct
    {
        public readonly Vector3 center;
        public readonly Vector3 radius;
        public T? data;
        public OctTreeNode<T>[] child =
            new OctTreeNode<T>[8];
        public int depth;
        public OctTreeNode<T>[] Child
        {
            get
            {
                for(int i =0;i<8;++i)
                {
                    if(null== child[i])
                    {
                        BuildOctTree(center,radius,depth);
                        break;
                    }
                }
                return child;
            }
        }
        public OctTreeNode(Vector3 center, Vector3 radius):base()
        {
            this.center = center;
            this.radius = radius;
            child = new OctTreeNode<T>[8];
        }

        public void BuildOctTree(
            Vector3 center,
            Vector3 r,
            int stopdepth)
        {
            if (stopdepth < 0) return;
            depth = stopdepth;
            r = r / 2; 
            for (int i = 0; i < 8; ++i)
            {
                Vector3 offset = Vector3.zero;
                float step;
                for (int j = 0; j < 3; ++j)
                {
                    step = r[j]/2;
                    offset[j] = (i & (1 << j)) > 0 ? step : -step;
                }
                child[i] = new OctTreeNode<T>(center + offset, r);
                child[i].BuildOctTree(center + offset, r, stopdepth - 1);
            }

        }

        public void Insert(T Pdata, Vector3 Pcenter, Vector3 Pradius)
        {
            if (0 == depth)
            {
                
                this.data = Pdata;
                return;
            }

            //for (int i = 0; i < 8; ++i)
            //{
            //    Child[i].Insert(Pdata, Pcenter, Pradius);
            //}
            Vector3 r = Pradius / 2;
            for (int i = 0; i < 8; ++i)
            {
                Vector3 offset = Vector3.zero;
                float step;
                for (int j = 0; j < 3; ++j)
                {
                    step = r[j] / 2;
                    offset[j] = (i & (1 << j)) > 0 ? step : -step;
                }
                Vector3 v = Pcenter + offset;
                if (!IsOutRange(v)) continue;
                for (int k = 0; k < 8; ++k)
                {
                    Child[k].Insert(Pdata, v, r);
                }
                

            }
        }

        /// <summary>
        /// 判断点是否在空间的内部
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private bool CheckPointIn(Vector3 point,Vector3 Pcenter, Vector3 Pradius)
        {
            for(int i =0;i<3;++i)
            {
                if (point[i] < Pcenter[i] - Pradius[i]) return false;
                if (point[i] > Pcenter[i] + Pradius[i]) return false;
            }
            return true;
        }
        /// <summary>
        /// 计算点所在子八叉树的索引
        /// </summary>
        /// <param name="Pcenter"></param>
        /// <returns></returns>
        private int GetOctTreeIndex(Vector3 Pcenter)
        {
            Vector3 v = Pcenter - center;
            Vector3 t = Vector3.zero;
            for (int i = 0; i < 3; ++i)
                t[i] = v[i] > 0 ? 1 : 0;
            return (int)(t[0] * 1 + t[1] * 2 + t[2] * 4 );
        }
        /// <summary>
        /// 是否超出范围
        /// </summary>
        /// <param name="Pcenter"></param>
        /// <returns></returns>
        private bool IsOutRange(Vector3 Pcenter)
        {
            Vector3 v = Pcenter - center;
            v = new Vector3(
                Math.Abs(v.x), Math.Abs(v.y), Math.Abs(v.z));
            bool xin = v.x >= 0 && v.x < radius.x;
            bool yin = v.y >= 0 && v.y < radius.y;
            bool zin = v.z >= 0 && v.z < radius.z;
            return xin&&yin&&zin;
        }
        
        public void Clear()
        {
            this.data = null;
            BuildOctTree(center, radius, depth);
        }
    }

    /// <summary>
    /// 八叉树
    /// </summary>
    public class OctTree
    {
        public OctTreeNode<AABB> AABBOctTree;
        public OctTreeNode<Sphere> SphereOctTree;
        public readonly Vector3 center;
        public readonly Vector3 radius;
        public OctTree(
            Vector3 center,
            Vector3 radius,
            int stopdepth)
        {
            this.center = center;
            this.radius = radius;          
            AABBOctTree = new OctTreeNode<AABB>
                (center, radius);
            SphereOctTree = new OctTreeNode<Sphere>
                (center, radius);
            if (stopdepth < 0) return;
            AABBOctTree.BuildOctTree(center, radius, stopdepth);
            SphereOctTree.BuildOctTree(center, radius, stopdepth);

        }

        public void Insert(AABB aabb)
        {
            AABBOctTree.Insert(aabb,aabb.center,aabb.radius);
        }
        public void Insert(Sphere sphere)
        {
            SphereOctTree.Insert(
                sphere,sphere.center,
                new Vector3(sphere.r, sphere.r, sphere.r));
        }
    }


}

