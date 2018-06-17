using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LinHoweAutonomousAction
{
    /// <summary>
    /// 减速到达
    /// </summary>
    public class Arrive:Steering
    {
        public GameObject target;

        //与目标间距离小于这个值，减速
        public float slowDownDistance = 1;
        public override Vector3 Force()
        {
            Vector3 direction = target.transform.position - transform.position;
            Vector3 desiredVelocity;
            if (direction.magnitude>slowDownDistance)
                desiredVelocity = direction.normalized * m_vehicle.maxSpeed;
            else
                desiredVelocity = direction - m_vehicle.velocity;
            return desiredVelocity - m_vehicle.velocity;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, slowDownDistance);
        }
    }
    
}
