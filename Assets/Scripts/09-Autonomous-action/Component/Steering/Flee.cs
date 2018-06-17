using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LinHoweAutonomousAction
{
    /// <summary>
    /// 逃跑
    /// </summary>
    public class Flee : Steering
    {
        public GameObject target;
        public float fearDistance;

        public override Vector3 Force()
        {
            if (null == target) return Vector3.zero;
            if(Vector3.Distance(target.transform.position, transform.position)>fearDistance)
                return Vector3.zero;
            Vector3 direction = target.transform.position - transform.position;
            Vector3 desiredVelocity = -direction.normalized * m_vehicle.maxSpeed;
            return desiredVelocity - m_vehicle.velocity;
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, fearDistance);
        }
    }
}
