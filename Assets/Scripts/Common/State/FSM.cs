using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSM : MonoBehaviour
{
    /// <summary>
    /// 状态机的当前状态
    /// </summary>
    protected State currentState { get; set; }

    /// <summary>
    /// 状态机存储的状态列表
    /// </summary>
    protected Dictionary<Type, State> states = new Dictionary<Type, State>();

    /// <summary>
    /// 对状态机添加一些状态
    /// </summary>
    public abstract void AddStates();

    /// <summary>
    /// 对状态机的初始化操作
    /// </summary>
    public virtual void Initialize()
    {
        AddStates();

        if (currentState == null)
        {
            throw new System.Exception("状态机：" + name + "的currentState没有设置，请通过SetCurrentState()函数设置！");
        }

        // 对状态机里的每个状态进行初始化
        foreach (KeyValuePair<Type, State> pair in states)
        {
            pair.Value.Initialize();
        }

        currentState.Enter();
    }

    /// <summary>
    /// 添加一个状态到状态机
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void AddState<T>() where T : State, new()
    {
        if (!ContainsState<T>())
        {
            State item = new T();
            item.Machine = this;

            states.Add(typeof(T), item);
        }
    }

    /// <summary>
    /// 转换状态
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void ChangeState<T>() where T : State
    {
        if (states[typeof(T)] == null)
        {
            throw new System.Exception("状态机查找不到状态：" + typeof(T).Name);
        }
        if (!currentState.IsTransition(typeof(T)))
        {
            throw new System.Exception("状态机查找不到状态转换条件：" + typeof(T).Name);
        }
        currentState.Exit();
        currentState = states[typeof(T)];
        currentState.Enter();
    }

    /// <summary>
    /// 检查状态机是否包含某个状态
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public bool ContainsState<T>() where T : State
    {
        return states.ContainsKey(typeof(T));
    }

    /// <summary>
    /// 获得状态机的当前状态
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T CurrentState<T>() where T : State
    {
        return (T)currentState;
    }

    /// <summary>
    /// 获取状态机某个状态
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetState<T>() where T : State
    {
        if (states[typeof(T)] == null)
        {
            throw new System.Exception("状态机：" + name + "查找不到状态：" + typeof(T).Name);
        }
        return (T)states[typeof(T)];
    }

    /// <summary>
    /// 判断某个状态是否为状态机当前状态
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public bool IsCurrentState<T>() where T : State
    {
        return (currentState.GetType() == typeof(T)) ? true : false;
    }
    public bool IsCurrentState(System.Type T) { return (currentState.GetType() == T) ? true : false; }


    /// <summary>
    /// 移除状态机所有状态
    /// </summary>
    public void RemoveAllStates()
    {
        states.Clear();
    }

    /// <summary>
    /// 移除状态机某个状态
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void RemoveState<T>() where T : State
    {
        states.Remove(typeof(T));
    }

    /// <summary>
    /// 设置状态机当前状态
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void SetCurrentState<T>() where T : State
    {
        currentState = states[typeof(T)];
    }

    public virtual void Start()
    {
        Initialize();
    }
    public virtual void Update()
    {

        currentState.Execute();
    }
    public virtual void FixedUpdate()
    {
        currentState.FixedExecute();
    }
    public virtual void LateUpdate()
    {
        currentState.LateExecute();
    }

    // 状态机传递一些特殊的操作给状态
    public void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(collision);
    }
    public void OnCollisionExit(Collision collision)
    {
        currentState.OnCollisionExit(collision);

    }
    public void OnCollisionStay(Collision collision)
    {
        currentState.OnCollisionStay(collision);
    }
    public void OnTriggerEnter(Collider collider)
    {
        currentState.OnTriggerEnter(collider);
    }
    public void OnTriggerExit(Collider collider)
    {
        currentState.OnTriggerExit(collider);
    }
    public void OnTriggerStay(Collider collider)
    {
        currentState.OnTriggerStay(collider);
    }


}
