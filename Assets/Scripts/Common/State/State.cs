using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    /// <summary>
    /// 存储状态能够转换的列表
    /// </summary>
    protected List<Type> list = new List<Type>();

    /// <summary>
    /// 添加可转换状态到列表中存储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void AddTransition<T>() where T : State
    {
        if (list.Contains(typeof(T)))
        {
            Debug.LogError("转换状态：" + typeof(T) + "已经有了对应的状态");
            return;
        }
        list.Add(typeof(T));
    }

    /// <summary>
    /// 从列表中删除某个可转换的状态
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void DeleteTransition<T>() where T : State
    {
        if (list.Contains(typeof(T)) == false)
        {
            Debug.LogWarning("转换条件：" + typeof(T) + "没有存在在map字典内！");
            return;
        }
        list.Remove(typeof(T));
    }

    /// <summary>
    /// 判断某个类型是否可转换
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public bool IsTransition(Type type)
    {
        if (list.Contains(type))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 状态是否启用
    /// </summary>
    public bool isActive
    {
        get { return Machine.IsCurrentState(GetType()); }
    }

    /// <summary>
    /// 获取控制当前状态的状态机
    /// </summary>
    public FSM Machine
    {
        get;
        internal set;
    }

    /// <summary>
    /// 状态初始化
    /// </summary>
    public virtual void Initialize()
    {

    }
    /// <summary>
    /// 进入状态时执行
    /// </summary>
    public virtual void Enter()
    {

    }
    /// <summary>
    /// 状态Update时执行
    /// </summary>
    public virtual void Execute()
    {

    }
    /// <summary>
    /// 退出状态时执行
    /// </summary>
    public virtual void Exit()
    {

    }
    /// <summary>
    /// 状态FixedUpdate时执行
    /// </summary>
    public virtual void FixedExecute()
    {

    }
    /// <summary>
    /// 状态LateUpdate时执行
    /// </summary>
    public virtual void LateExecute()
    {

    }

    public virtual void OnCollisionEnter(Collision collision)
    {

    }
    public virtual void OnCollisionExit(Collision collision)
    {

    }
    public virtual void OnCollisionStay(Collision collision)
    {

    }
    public virtual void OnTriggerEnter(Collider collider)
    {

    }
    public virtual void OnTriggerExit(Collider collider)
    {

    }
    public virtual void OnTriggerStay(Collider collider)
    {

    }
}
