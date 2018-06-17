using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LinHoweAutonomousAction
{
    /// <summary>
    /// 躲避障碍
    /// </summary>
    public class DodgeObstacles:Steering
    {
        public float maxseeDistance = 2;
        public float avoidanceForce = 1;
        public override Vector3 Force()
        {
            Vector3 force = Vector3.zero;
            Vector3 normalV = m_vehicle.velocity.normalized;
            float m = m_vehicle.velocity.magnitude / m_vehicle.maxSpeed;
            Vector3 ahead = transform.position + normalV * maxseeDistance * m;
            Debug.DrawLine(transform.position,
                ahead);
            RaycastHit hit;
            if(Physics.Raycast(transform.position, normalV,out hit, m))
            {
                force = ahead - hit.transform.position;
                force *= avoidanceForce;
            }

            return force;
        }
    }
}
