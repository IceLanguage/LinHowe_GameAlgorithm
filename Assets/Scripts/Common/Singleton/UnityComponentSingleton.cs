using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 为Unity组件设计的单例模式模板类
/// </summary>
/// <typeparam name="T"></typeparam>
public class UnityComponentSingleton<T>:MonoBehaviour
    where T : Component
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (null == _instance )
            {
                _instance = FindObjectOfType(typeof(T)) as T;
                if (null == _instance )
                {
                    GameObject obj = new GameObject();
                    obj.hideFlags = HideFlags.HideAndDontSave;//隐藏实例化的new game object，下同  
                    _instance = obj.AddComponent(typeof(T)) as T;
                }
            }
            return _instance;
        }
    }
}

