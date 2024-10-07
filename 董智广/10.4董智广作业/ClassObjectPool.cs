using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 类对象池
/// </summary>
/// <typeparam name="T"></typeparam>
public class ClassObjectPool<T> where T :class, new()
{
    /// <summary>
    /// 池
    /// </summary>
    protected Stack<T> m_Pool = new Stack<T>();//500 classs
    /// <summary>
    /// 最大对象个数，<=0表示不限个数
    /// </summary>
    protected int m_MaxCount = 0;   //500 500, 
    /// <summary>
    /// 没有回收的对象个数
    /// </summary>
    protected int noRecycleCount = 0;

    public ClassObjectPool(int maxcount)
    {
        m_MaxCount = maxcount;
        for (int i = 0; i < maxcount; i++)
        {
            m_Pool.Push(new T());// 
        }
    }

    /// <summary>
    /// 从池里面取类对象
    /// </summary>
    /// <param name="creatIfPoolEmpty">如果为空是否new出来</param>
    /// <returns></returns>
    public T Spawn(bool creatIfPoolEmpty)
    {
        if (m_Pool.Count > 0)
        {
            T rtn = m_Pool.Pop();
            if (rtn == null)
            {
                if (creatIfPoolEmpty)
                    rtn = new T();
            }
            noRecycleCount++;
            return rtn;
        }
        else
        {
            if (creatIfPoolEmpty)
            {
                T rtn = new T();
                noRecycleCount++;
                return rtn;
            }
        }

        return null;
    }

    /// <summary>
    /// 回收类对象
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public bool Recycle(T obj)
    {
        if (obj == null)
            return false;

        noRecycleCount--;
        
        if (m_Pool.Count >= m_MaxCount && m_MaxCount > 0)
        {
            obj = null;
            return false;
        }

        m_Pool.Push(obj);
        return true;
    }
}
