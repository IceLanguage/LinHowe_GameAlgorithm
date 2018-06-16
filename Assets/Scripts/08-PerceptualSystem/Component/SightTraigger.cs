using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LinhowePerceptualSystem
{
    /// <summary>
    /// 视觉触发器
    /// </summary>
    public class SightTraigger:BaseTraigger
    {       

        protected override void Awake()
        {
            PerceptualTyep = PerceptualEnum.Sight;
            base.Awake();
           
        }

        protected override bool IsTraigger(BaseSensor sensor)
        {
            RaycastHit hit;
            Vector3 rayDirection = transform.position - sensor.transform.position;
            rayDirection.y = 0;
            float angle = Vector3.Angle(rayDirection, sensor.transform.forward);
            SightSensor sight = sensor as SightSensor;
            float fieldView = sight.fieldView;

            //检查是否在视域内
            if(angle<fieldView)
            {
                //检查是否存在障碍物
                if(Physics.Raycast(sensor.transform.position + Vector3.up,
                    rayDirection,out hit,sight.viewDistance))
                {
                    if (hit.collider.gameObject == gameObject)
                        return true;
                }
            }

            return false;
        }

    }
}
