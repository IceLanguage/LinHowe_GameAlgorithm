using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LinhowePerceptualSystem
{
    /// <summary>
    /// 听觉触发器
    /// </summary>
    public class SoundTraigger:LimitedTimeTraigger
    {
        public float radius;
        protected override void Awake()
        {
            PerceptualTyep = PerceptualEnum.Sound;
            base.Awake();

        }

        protected override bool IsTraigger(BaseSensor sensor)
        {
            if(Vector3.Distance(sensor.transform.position,transform.position)
                <radius)
            {
                return true;
            }
            return false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.gray;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
