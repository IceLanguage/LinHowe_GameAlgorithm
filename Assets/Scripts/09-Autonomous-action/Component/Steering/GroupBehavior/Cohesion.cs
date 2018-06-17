using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LinHoweAutonomousAction
{ 
    /// <summary>
    /// 聚集
    /// </summary>
    public class Cohesion:Steering
    {
        private Radar radar;
        private void Start()
        {
            radar = GetComponent<Radar>();
        }
        public override Vector3 Force()
        {
            Vector3 force = Vector3.zero;
            Vector3 centerMass = Vector3.zero;
            var neighbors = radar.Neighbors;

            int neighborCount = 0;
            foreach (var e in neighbors)
            {
                if (null != e && e != gameObject)
                {
                    ++neighborCount;

                    centerMass += e.transform.position;
                }
            }
            if(neighborCount>0)
            {
                centerMass /= neighborCount;
                Vector3 desiredVelocity = (centerMass - transform.position).normalized * m_vehicle.maxSpeed;
                force = desiredVelocity - m_vehicle.velocity;
            }
            return force;
        }
    }
}
