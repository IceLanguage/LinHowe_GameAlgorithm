using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LinHoweAutonomousAction
{
    /// <summary>
    /// 靠近
    /// </summary>
    public class Seek:Steering
    {
        
        public GameObject target;
        
        public override Vector3 Force()
        {
            if (null == target) return Vector3.zero;

            Vector3 direction = target.transform.position - transform.position;
            Vector3 desiredVelocity = direction.normalized * m_vehicle.maxSpeed;
            return desiredVelocity - m_vehicle.velocity;
        }
    }
}
