using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LinHoweAutonomousAction
{
    public class Evade : Steering
    {
        public GameObject target;
        private Vehicle targetVehicle;
        private void Start()
        {
            targetVehicle = target.GetComponent<Vehicle>();
        }
        public override Vector3 Force()
        {
            if (null == target) return Vector3.zero;
            Vector3 direction = target.transform.position - transform.position;
            //计算预测时间
            float lookaheadTime = direction.magnitude / (m_vehicle.maxSpeed + targetVehicle.velocity.magnitude);

            Vector3 desiredVelocity = (transform.position - (target.transform.position+ targetVehicle.velocity) * lookaheadTime).normalized * m_vehicle.maxSpeed;
            return desiredVelocity - m_vehicle.velocity;
        }

    }
}
