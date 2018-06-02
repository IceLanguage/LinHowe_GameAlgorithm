using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyUnityEventDispatcher
{
    /// <summary>
    /// 定义事件分发委托,T是传输的数据
    /// </summary>
    public delegate void OnNotification<T>(Notification<T> notific);

    /// <summary>
    /// 通知中心
    /// </summary>
    public class NotificationCenter<T>
    {
        /// <summary>
        /// 通知中心单例
        /// </summary>
        public static NotificationCenter<T> _instance;
        public static NotificationCenter<T>  Get()
        {
            
            
            if(null == _instance)
            {
                _instance = new NotificationCenter<T>();
            }
            return _instance;
            
        }

        /// <summary>
        /// 存储事件的字典
        /// </summary>
        private Dictionary<string, OnNotification<T>> eventListeners
        = new Dictionary<string, OnNotification<T>>();

        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="eventKey">事件Key</param>
        /// <param name="eventListener">事件监听器</param>
        public void AddEventListener(string eventKey, OnNotification<T> eventListener)
        {
            if (!eventListeners.ContainsKey(eventKey))
            {
                eventListeners.Add(eventKey, eventListener);
            }
            else
            {
                eventListeners[eventKey] += eventListener;
            }
        }

        /// <summary>
        /// 取消监听
        /// </summary>
        /// <param name="eventKey"></param>
        /// <param name="eventListener"></param>
        public void RemoveEventListener(string eventKey, OnNotification<T> eventListener)
        {
            if (!eventListeners.ContainsKey(eventKey))
            {
                return;
            }
            else
            {
                eventListeners[eventKey] -= eventListener;
            }
        }

        /// <summary>
        /// 移除事件
        /// </summary>
        /// <param name="eventKey">事件Key</param>
        public void RemoveEventListener(string eventKey)
        {
            if (!eventListeners.ContainsKey(eventKey))
                return;

            eventListeners[eventKey] = null;
            eventListeners.Remove(eventKey);
        }

        /// <summary>
        /// 分发事件
        /// </summary>
        /// <param name="eventKey">事件Key</param>
        /// <param name="notific">通知</param>
        public void DispatchEvent(string eventKey, Notification<T> notific)
        {
            if (!eventListeners.ContainsKey(eventKey))
                return;
            eventListeners[eventKey](notific);
        }

        /// <summary>
        /// 分发事件
        /// </summary>
        /// <param name="eventKey">事件Key</param>
        /// <param name="sender">发送者</param>
        /// <param name="param">通知内容</param>
        public void DispatchEvent(string eventKey, GameObject sender, T param)
        {
            if (!eventListeners.ContainsKey(eventKey))
                return;
            eventListeners[eventKey](new Notification<T>(sender, param));
        }

        /// <summary>
        /// 分发事件
        /// </summary>
        /// <param name="eventKey">事件Key</param>
        /// <param name="param">通知内容</param>
        public void DispatchEvent(string eventKey, T param)
        {
            if (!eventListeners.ContainsKey(eventKey))
                return;
            eventListeners[eventKey](new Notification<T>(param));
        }

        /// <summary>
        /// 是否存在指定事件的监听器
        /// </summary>
        public Boolean HasEventListener(string eventKey)
        {
            return eventListeners.ContainsKey(eventKey);
        }
    }
}
