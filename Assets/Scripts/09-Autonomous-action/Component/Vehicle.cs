using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LinHoweAutonomousAction
{
    /// <summary>
    /// 可移动物体基类
    /// </summary>
    public class Vehicle : MonoBehaviour
    {
        //AI角色操控行为列表
        private Steering[] steerings;

        public float maxSpeed = 10;
        public float maxForce = 100;
        public float mass = 1;
        //protected Vector3 velocity;
        public Vector3 velocity { get; set; }
        //转向的速度
        public float damping = 0.9f;
        //操作力的计算间隔时间
        public float computeInterval = 0.2f;

        //是否在二维平面
        //public bool isPlanar = true;

        //计算得到的操控力
        protected Vector3 steeringForce = Vector3.zero;
        //加速度
        protected Vector3 acceleration;
        //计时器
        protected float timer;

        protected void Awake()
        {
            timer = 0;
            steerings = GetComponents<Steering>();
        }

        protected virtual void Update()
        {
            //计算加速度
            timer += Time.deltaTime;
            steeringForce = Vector3.zero;
            if(timer>computeInterval)
                foreach (Steering s in steerings)
                    if (s.enabled)
                        steeringForce += s.Force() * s.weight;
            steeringForce = Vector3.ClampMagnitude(steeringForce, maxForce);
            acceleration = steeringForce / mass;
            timer = 0;
        }
    }
}

