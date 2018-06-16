using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LinHoweAutonomousAction
{
    /// <summary>
    /// AI移动控制类
    /// </summary>
    public class AILocomotion:Vehicle
    {
        protected Rigidbody m_rigidbody;
        protected Vector3 moveDistance;
        protected Animator animator;

        protected void Start()
        {
            m_rigidbody = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
            moveDistance = Vector3.zero;
        }

        protected void FixedUpdate()
        {
            velocity += acceleration * Time.fixedDeltaTime;
            if (velocity.magnitude > maxSpeed)
                velocity = velocity.normalized * maxSpeed;
            moveDistance = velocity * Time.fixedDeltaTime;

            transform.position += moveDistance;

            //更新朝向，防止抖动
            if(velocity.magnitude>float.Epsilon)
            {
                Vector3 newForward = Vector3.Slerp(transform.forward, velocity, damping * Time.fixedDeltaTime);

                transform.forward = newForward;
            }
            

        }
    }
}
