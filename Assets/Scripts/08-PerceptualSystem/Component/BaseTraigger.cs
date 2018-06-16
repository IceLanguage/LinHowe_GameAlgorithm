using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LinhowePerceptualSystem
{
    /// <summary>
    /// 触发器基类
    /// </summary>
    public class BaseTraigger : MonoBehaviour
    {
        public bool ToBeRemoved = false;
        protected virtual void Awake()
        {
            InitTrigger();
        }
        protected virtual void InitTrigger()
        {
            PerceptualSystemManager.RegisterTrigger(this);
        }
        public PerceptualEnum PerceptualTyep { get; set; }
        public virtual void UpdateInfo() { }
        public void Try(BaseSensor sensor)
        {
            if (PerceptualTyep == sensor.PerceptualTyep && IsTraigger(sensor))
            {
                sensor.Notify(this);
            }
        }
        protected virtual bool IsTraigger(BaseSensor sensor) { return false; }
    }

    
}