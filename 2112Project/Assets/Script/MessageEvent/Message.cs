using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message
{
    //���ͣ�ʹ��ö������EventTyppe
    public MessageType type;
    //���ݣ�ʹ��object���ͣ����Դ洢�κ����͵�����
    public object data;

    public Message(MessageType type, object data)
    {
        this.type = type;
        this.data = data;
    }
}


/// <summary>
/// ����
/// </summary>
public enum MessageType
{
    Login=0,
    Game_Start=1,
    TableChanged=2,
    ChangedText=3,

    //�ʼ�
    OnResAllMailInfoMessage,
    OnResReadMailMessage,
    OnResReceiveMailMessage,
    OnResGetMailAttachMessage,
    OnResGetAllMailAttachMessage,
    OnUpdateMailListEvent,

    Transaction = 4,//����
    Transaction_Succed = 5,//���׳ɹ�
    RemoveListGood = 6//�Ƴ������б������Ʒ
}