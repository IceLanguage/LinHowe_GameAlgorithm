using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using CollisionDetection;

namespace LinHoweCollisionDetection
{
    public class AABBComponent : MonoBehaviour
    {
        //public Vector3 center;
        public Vector3 radius;
        private AABB aabb;
        private void Awake()
        {
            aabb = new AABB(transform.position, radius);
            GameManager.Instance.aabbs.Add(aabb);
        }
        public void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(0.2f, 0.4f, 0.4f);
            Gizmos.DrawCube(transform.position, radius);
     
        }
    }
}
