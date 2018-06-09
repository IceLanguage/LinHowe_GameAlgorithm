using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using CollisionDetection;

namespace LinHoweCollisionDetection
{
    public class OctTreeComponent: UnityComponentSingleton<OctTreeComponent>
    {
        public OctTree tree;
        public Vector3 radius;
        public int depth;
        private void Awake()
        {
            tree = new OctTree(transform.position, radius, depth);
        }

        private void OnDrawGizmos()
        {
            if (null == tree) return;
            Gizmos.color = new Color(0.9f, 0.6f, 0.1f);
            Gizmos.DrawWireCube(transform.position, radius);
            DrawCub(tree.AABBOctTree);


        }
        private void DrawCub(OctTreeNode<AABB> tree)
        {
            //Gizmos.color = new Color(0.9f, 0.6f, 0.1f);
            if (null == tree) return;
            //if (0 == tree.depth&&null == tree.data)
            //    Gizmos.DrawWireCube(tree.center, tree.radius);
            if (null != tree.data)
            {
                Gizmos.color = new Color(1f, 1f, 1f);
                Gizmos.DrawCube(tree.center, tree.radius);
            }
                
            for (int i = 0; i < tree.child.Length; ++i)
            {
                DrawCub(tree.child[i]);
            }
        }
        private void Update()
        {
            
        }
    }
}
