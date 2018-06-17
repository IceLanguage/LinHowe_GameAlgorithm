using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LinHoweAutonomousAction
{
    /// <summary>
    /// 追逐
    /// </summary>
    public class Chase:Steering
    {
        public GameObject target;
        private Vehicle targetVehicle;
        private void Start()
        {
            targetVehicle = target.GetComponent<Vehicle>();
        }
        public override Vector3 Force()
        {
            if (null == target||null == targetVehicle) return Vector3.zero;

            Vector3 direction = target.transform.position - transform.position;

            //计算夹角
            float relativeDirection = Vector3.Dot(transform.forward, target.transform.forward);
            Vector3 desiredVelocity = Vector3.zero;
            if (Vector3.Dot(direction,transform.forward)>0&&relativeDirection>-0.95f)
            {
               desiredVelocity = direction.normalized * m_vehicle.maxSpeed;
                return desiredVelocity - m_vehicle.velocity;
            }

            //计算预测时间
            float lookaheadTime = direction.magnitude / (m_vehicle.maxSpeed + targetVehicle.velocity.magnitude);

            desiredVelocity =( target.transform.position + targetVehicle.velocity * lookaheadTime).normalized * m_vehicle.maxSpeed;
            return desiredVelocity - m_vehicle.velocity;
        }
    }
}
