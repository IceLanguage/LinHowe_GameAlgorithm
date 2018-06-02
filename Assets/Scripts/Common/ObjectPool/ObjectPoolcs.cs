using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ObjectPool_Queue<T> where T : class, new()
{
    private Queue<T> m_objectQueue;

    private Action<T> m_ResetAction;

    private Action<T> m_InitAction;

    public ObjectPool_Queue(Action<T>
        ResetAction = null, Action<T> OnetimeInitAction = null)
    {
        m_objectQueue = new Queue<T>();
        m_ResetAction = ResetAction;
        m_InitAction = OnetimeInitAction;
    }

    public T New()
    {
        if (m_objectQueue.Count > 0)
        {
            T t = m_objectQueue.Dequeue();

            if (null != m_ResetAction )
                m_ResetAction(t);

            if (null != m_InitAction)
                m_InitAction(t);

            return t;
        }
        else
        {
            return null;
        }
    }

    public void Store(T obj)
    {
        m_objectQueue.Enqueue(obj);
    }

}

public class ObjectPool_Dict<Key, Value>
    where Key : struct
    where Value : class, new()
{
    private Dictionary<Key, ObjectPool_Queue<Value>> m_objectDic;
    private readonly Action<Value> m_ResetAction;
    private readonly Action<Value> m_InitAction;

    public ObjectPool_Dict(Action<Value>
        ResetAction = null, Action<Value> OnetimeInitAction = null)
    {
        //m_MaxTIme = initialBufferSize;
        m_objectDic = new Dictionary<Key, ObjectPool_Queue<Value>>();

        m_ResetAction = ResetAction;
        m_InitAction = OnetimeInitAction;

    }

    public Value this[Key key]
    {
        get
        {
            if (!m_objectDic.ContainsKey(key)) return null;
            var objectPool_Queue = m_objectDic[key];
            if (null == objectPool_Queue)
                objectPool_Queue = new ObjectPool_Queue<Value>(m_ResetAction, m_InitAction);
            var t = objectPool_Queue.New();
            if (null == t) return t;

            return t;
        }
        set
        {
            if (!m_objectDic.ContainsKey(key)|| null == m_objectDic[key])
            {
                m_objectDic[key] = new ObjectPool_Queue<Value>(m_ResetAction, m_InitAction);
            } 
            m_objectDic[key].Store(value);

        }
    }


   
}
