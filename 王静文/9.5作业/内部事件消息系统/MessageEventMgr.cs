using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

/// <summary>
/// 内部事件消息管理类
/// 要在对应的具体UI功能里去把消息关联的回调函数进行绑定
/// 进行消息的派发(Dispatch)和侦听(AddListener)
/// </summary>

//事件监听器委托，用于定义事件处理函数的签名
public delegate void EventListenerDelegate(Message evt);
public class MessageEventMgr
{
    static MessageEventMgr instance;

    public static MessageEventMgr GetInstance()
    {
        if(instance == null)
        {
            instance = new MessageEventMgr();
        }
        return instance;
    }

    //存储事件类型和对应事件监听器的映射关系
    private Dictionary<MessageType, EventListenerDelegate> events = new Dictionary<MessageType, EventListenerDelegate>();

    /// <summary>
    /// 订阅
    /// </summary>
    /// <param name="type">事件类型</param>
    /// <param name="listener">事件监听器委托</param>
    public void AddListener(MessageType type, EventListenerDelegate listener)
    {
        if (listener == null)
        {
            Debug.LogError("AddListener: listener不能为空");
            return;
        }

        EventListenerDelegate myListener = null;
        //检查字典中是否已经存在该事件类型的监听器
        events.TryGetValue(type, out myListener);
        //如果存在，使用Delegate.Combine组合现有的监听器和新的监听器
        //如果不存在，直接添加新的监听器
        events[type] = (EventListenerDelegate)Delegate.Combine(myListener, listener);
    }

    /// <summary>
    /// 取消订阅
    /// </summary>
    /// <param name="type">事件类型</param>
    /// <param name="listener">监听器的委托</param>
    public void RemoveListener(MessageType type, EventListenerDelegate listener)
    {
        if (listener == null)
        {
            Debug.LogError("RemoveListener: listener不能为空");
            return;
        }
        //使用Delegate.Remove从已有的监听器列表中移除指定的监听器
        events[type] = (EventListenerDelegate)Delegate.Remove(events[type], listener);
    }

    /// <summary>
    /// 用于派发消息
    /// </summary>
    /// <param name="type">事件类型</param>
    /// <param name="param">事件参数</param>
    public void Dispatch(MessageType type, object param)
    {
        EventListenerDelegate listenerDelegate;
        //从字典中获取事件类型对应的监听器
        if (events.TryGetValue(type, out listenerDelegate))
        {
            //创建事件数据实例
            Message evt = new Message(type, param);
            try
            {
                //如果监听器存在，调用事件监听器
                if (listenerDelegate != null)
                {
                    listenerDelegate(evt);
                }
            }
            catch (System.Exception e)
            {
                //捕获并记录可能的异常
                Debug.Log("SendMessage:" + evt.type.ToString() + " " + e.Message + " " + e.StackTrace + " " + e);
            }
        }
    }

    //清空事件字典，取消所有事件监听
    public void Clear()
    {
        events.Clear();
    }
}
