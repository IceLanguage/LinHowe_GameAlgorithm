using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyUnityEventDispatcher
{
    public class Notification<T>
        
    {
        /// <summary>
        ///通知发送者
        /// </summary>
        public GameObject sender;

        /// <summary>
        /// 限制参数类型
        /// </summary>
        public T param;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sender">通知发送者</param>
        /// <param name="param">通知内容</param>
        public Notification(GameObject sender,T param)
        {
            this.sender = sender;
            this.param = param;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="param"></param>
        public Notification(T param)
        {
            this.sender = null;
            this.param = param;
        }

        
        /// <summary>
        /// 实现ToString方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("sender={0},param={1}", this.sender, this.param);
        }
    }
}

    

