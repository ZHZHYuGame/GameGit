using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message
{
    //事件类型，使用枚举类型EventTyppe
    public MessageType type;
    //事件数据，使用object类型，可以存储任何类型的数据
    public object data;

    public Message(MessageType type, object data)
    {
        this.type = type;
        this.data = data;
    }
}


/// <summary>
/// 事件类型
/// </summary>
public enum MessageType
{
    Login=0,
    Game_Start=1,
    TableChanged=2,
    ChangedText=3,
    OnUpdateMailListEvent,
}