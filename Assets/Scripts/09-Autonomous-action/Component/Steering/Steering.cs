using UnityEngine;
using System.Collections;

namespace LinHoweAutonomousAction
{
    /// <summary>
    /// 操作行为的基类
    /// </summary>
    public abstract class Steering : MonoBehaviour
    {
        //操作力的权重
        public float weight = 1f;

        //计算操作力的方法
        public virtual Vector3 Force() { return Vector3.zero; }

        protected Vehicle m_vehicle;
        protected void Awake()
        {
            m_vehicle = GetComponent<Vehicle>();
        }
    }
}

