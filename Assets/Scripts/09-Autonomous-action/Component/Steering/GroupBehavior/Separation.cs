using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LinHoweAutonomousAction
{
    /// <summary>
    /// 分离
    /// </summary>
    [RequireComponent(typeof(Radar))]
    public class Separation:Steering
    {
        private Radar radar;

        //距离
        public float comfortDistance = 1;

        //斥力的系数
        public float multiplierInsideComfortDistance = 2;
        private void Start()
        {
            radar = GetComponent<Radar>();
        }
        public override Vector3 Force()
        {
            Vector3 force = Vector3.zero;

            var neighbors = radar.Neighbors;
            foreach (var e in neighbors)
            {
                if(null != e && e != gameObject)
                {
                    Vector3 toneighbor = transform.position - e.transform.position;

                    //邻居引起的排斥力
                    force += toneighbor.normalized / toneighbor.magnitude;

                    if (toneighbor.magnitude < comfortDistance)
                        force *= multiplierInsideComfortDistance;
                }
            }
            return force;
        }
    }
}
