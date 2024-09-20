using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

/// <summary>
/// �ڲ��¼���Ϣ������
/// Ҫ�ڶ�Ӧ�ľ���UI������ȥ����Ϣ�����Ļص��������а�
/// ������Ϣ���ɷ�(Dispatch)������(AddListener)
/// </summary>

//�¼�������ί�У����ڶ����¼���������ǩ��
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

    //�洢�¼����ͺͶ�Ӧ�¼���������ӳ���ϵ
    private Dictionary<MessageType, EventListenerDelegate> events = new Dictionary<MessageType, EventListenerDelegate>();

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="type">�¼�����</param>
    /// <param name="listener">�¼�������ί��</param>
    public void AddListener(MessageType type, EventListenerDelegate listener)
    {
        if (listener == null)
        {
            Debug.LogError("AddListener: listener����Ϊ��");
            return;
        }

        EventListenerDelegate myListener = null;
        //����ֵ����Ƿ��Ѿ����ڸ��¼����͵ļ�����
        events.TryGetValue(type, out myListener);
        //������ڣ�ʹ��Delegate.Combine������еļ��������µļ�����
        //��������ڣ�ֱ������µļ�����
        events[type] = (EventListenerDelegate)Delegate.Combine(myListener, listener);
    }

    /// <summary>
    /// ȡ������
    /// </summary>
    /// <param name="type">�¼�����</param>
    /// <param name="listener">��������ί��</param>
    public void RemoveListener(MessageType type, EventListenerDelegate listener)
    {
        if (listener == null)
        {
            Debug.LogError("RemoveListener: listener����Ϊ��");
            return;
        }
        //ʹ��Delegate.Remove�����еļ������б����Ƴ�ָ���ļ�����
        events[type] = (EventListenerDelegate)Delegate.Remove(events[type], listener);
    }

    /// <summary>
    /// �����ɷ���Ϣ
    /// </summary>
    /// <param name="type">�¼�����</param>
    /// <param name="param">�¼�����</param>
    public void Dispatch(MessageType type, object param)
    {
        EventListenerDelegate listenerDelegate;
        //���ֵ��л�ȡ�¼����Ͷ�Ӧ�ļ�����
        if (events.TryGetValue(type, out listenerDelegate))
        {
            //�����¼�����ʵ��
            Message evt = new Message(type, param);
            try
            {
                //������������ڣ������¼�������
                if (listenerDelegate != null)
                {
                    listenerDelegate(evt);
                }
            }
            catch (System.Exception e)
            {
                //���񲢼�¼���ܵ��쳣
                Debug.Log("SendMessage:" + evt.type.ToString() + " " + e.Message + " " + e.StackTrace + " " + e);
            }
        }
    }

    //����¼��ֵ䣬ȡ�������¼�����
    public void Clear()
    {
        events.Clear();
    }
}
