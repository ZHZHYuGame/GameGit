using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message
{
    //�¼����ͣ�ʹ��ö������EventTyppe
    public MessageType type;
    //�¼����ݣ�ʹ��object���ͣ����Դ洢�κ����͵�����
    public object data;

    public Message(MessageType type, object data)
    {
        this.type = type;
        this.data = data;
    }
}


/// <summary>
/// �¼�����
/// </summary>
public enum MessageType
{
    Login=0,
    Game_Start=1,
    TableChanged=2,
    ChangedText=3,
    OnUpdateMailListEvent,
}