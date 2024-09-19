using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 内部事件消息系统的测试：订阅和取消订阅
/// </summary>
public class Test : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.eventMgr.AddListener(MessageType.Login, OnUIFastEvent);
    }
    private void OnDisable()
    {
        GameManager.eventMgr.RemoveListener(MessageType.Login, OnUIFastEvent);
    }


    /// <summary>
    /// 消息的回调
    /// </summary>
    /// <param name="message">数据</param>
    private void OnUIFastEvent(Message message)
    {
        Debug.Log("当前事件类型 ：" + message.type+ "   内容 ：" + message.data);
    }
}
