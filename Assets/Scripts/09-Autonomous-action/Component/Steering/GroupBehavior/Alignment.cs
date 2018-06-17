using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LinHoweAutonomousAction
{
    /// <summary>
    /// 队列
    /// </summary>
    public class Alignment:Steering
    {
        private Radar radar;
        private void Start()
        {
            radar = GetComponent<Radar>();
        }
        public override Vector3 Force()
        {
            Vector3 force = Vector3.zero;

            var neighbors = radar.Neighbors;

            int neighborCount = 0;
            foreach (var e in neighbors)
            {
                if (null != e && e != gameObject)
                {
                    ++neighborCount;

                    force += e.transform.position;
                }
            }
            if(neighborCount>0)
            {
                force /= neighborCount;
                force -= transform.position;
            }
            return force;
        }
    }
}
