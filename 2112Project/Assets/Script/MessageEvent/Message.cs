using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message
{
    //类型，使用枚举类型EventTyppe
    public MessageType type;
    //数据，使用object类型，可以存储任何类型的数据
    public object data;

    public Message(MessageType type, object data)
    {
        this.type = type;
        this.data = data;
    }
}


/// <summary>
/// 类型
/// </summary>
public enum MessageType
{
    Login=0,
    Game_Start=1,
    TableChanged=2,
    ChangedText=3,

    //邮件
    OnResAllMailInfoMessage,
    OnResReadMailMessage,
    OnResReceiveMailMessage,
    OnResGetMailAttachMessage,
    OnResGetAllMailAttachMessage,
    OnUpdateMailListEvent,

    Transaction = 4,//交易
    Transaction_Succed = 5,//交易成功
    RemoveListGood = 6//移除交易列表里的物品
}