using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonMonobehavior<T> : MonoBehaviour where T : MonoBehaviour
{

    private static T _Instance; 
    public static T Instance
    {

        get
        {
            if (!IsInstantiated())
            {
                Instantiate();
            }
            return _Instance;
        }
        set
        {
            _Instance = value;
        }

    }
    public static bool IsInstantiated()
    {
        return null != _Instance ;
    }



    public delegate void OnInstantiateCompletedDelegate(T instance);
    public static OnInstantiateCompletedDelegate OnInstantiateCompleted;

    public delegate void OnDestroyingDelegate(T instance);
    public static OnDestroyingDelegate OnDestroying;

    /// <summary>
    /// Instantiate this instance. 
    /// 	1. Attempts to find an existing GameObject that matches (There will be 0 or 1 at any time)
    /// 	2. Creates GameObject with name of subclass
    /// 	3. Persists by default (optional)
    /// 	4. Predictable life-cycle.
    /// 
    /// </summary>
    public static T Instantiate()
    {

        if (!IsInstantiated())
        {
            T t = GameObject.FindObjectOfType<T>();
            GameObject go = null;
            if (null != t )
            {
                go = t.gameObject;
            }

            if (null == go)
            {
                go = new GameObject();
                _Instance = go.AddComponent<T>();
            }
            else
            {
                _Instance = go.GetComponent<T>();
            }

            go.name = _Instance.GetType().Name;
            DontDestroyOnLoad(go);

            if (null != OnInstantiateCompleted )
            {
                OnInstantiateCompleted(_Instance);
            }
        }
        return _Instance;
    }

    protected virtual void Awake()
    {
        Instantiate();
    }
    protected virtual void OnDestroy()
    {

    }
    public static void Destroy()
    {

        if (IsInstantiated())
        {
            if (OnDestroying != null)
            {
                OnDestroying(_Instance);
            }
            DestroyImmediate(_Instance.gameObject);

            _Instance = null;
        }
    }

    

}
