using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LinhowePerceptualSystem
{
    /// <summary>
    /// 感知器基类
    /// </summary>
    public class BaseSensor : MonoBehaviour
    {
        public bool ToBeRemoved = false;
        protected Animator animator;
        protected Rigidbody _rigidbody;
        protected virtual void Awake()
        {
            InitSensor();
        }
        protected virtual void InitSensor()
        { 
            PerceptualSystemManager.RegisterSensor(this);
        }
        public PerceptualEnum PerceptualTyep{ get; set; }
        public virtual void Notify(BaseTraigger traigger) { }

    }
}
