using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LinHoweAutonomousAction
{
    /// <summary>
    /// 徘徊
    /// </summary>
    public class Wander:Steering
    {
        //徘徊圆的半径
        public float WanderRadius;

        //徘徊圈突出在AI角色前面的距离
        public float WanderDistance;

        //每秒加到目标的随机位移的最大值
        public float WanderJitter;

        private Vector3 circleTarget;
        private void Start()
        {
            circleTarget = new Vector3(WanderRadius, 0, WanderRadius);
        }

        public override Vector3 Force()
        {
            //计算随机位移
            Vector3 randomDisplacement = new Vector3
                ((UnityEngine.Random.value - 0.5f)* WanderJitter, 
                (UnityEngine.Random.value - 0.5f) * WanderJitter,
                (UnityEngine.Random.value - 0.5f) * WanderJitter);

            circleTarget += randomDisplacement;

            //由于新位置可能不在圆周上，需要投影到圆周上
            circleTarget = WanderRadius * circleTarget.normalized;

    

            Vector3 desiredVelocity = (
                m_vehicle.velocity.normalized * WanderDistance +
                circleTarget).normalized * m_vehicle.maxSpeed;
            return desiredVelocity - m_vehicle.velocity;
        }

    }
}
