using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LinHoweAutonomousAction
{
    /// <summary>
    /// 路径跟踪
    /// </summary>
    public class FollowingPath:Steering
    {
        public GameObject[] wayPoint;
        public float slowDownDistance;
        private Transform target;
        private int curnode = 0;
        private void Start()
        {
            target = wayPoint[curnode].transform;
        }
        public override Vector3 Force()
        {
            Vector3 direction = target.position - transform.position;
            Vector3 desiredVelocity = Vector3.zero;
            if (curnode == wayPoint.Length-1)
            {
                if (direction.magnitude > slowDownDistance)
                    desiredVelocity = direction.normalized * m_vehicle.maxSpeed;
                else
                {
                    desiredVelocity = direction.normalized - m_vehicle.velocity;
                    curnode = 0;
                    target = wayPoint[curnode].transform;
                }
                  

            }
            else
            {
                desiredVelocity = direction.normalized * m_vehicle.maxSpeed;
                if (direction.magnitude<slowDownDistance)
                {
                    ++curnode;
                    target = wayPoint[curnode].transform;
                }
                
            }
            return desiredVelocity - m_vehicle.velocity;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, slowDownDistance);
        }
    }
}
