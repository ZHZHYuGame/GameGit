using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ڲ��¼���Ϣϵͳ�Ĳ��ԣ����ĺ�ȡ������
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
    /// ��Ϣ�Ļص�
    /// </summary>
    /// <param name="message">����</param>
    private void OnUIFastEvent(Message message)
    {
        Debug.Log("��ǰ�¼����� ��" + message.type+ "   ���� ��" + message.data);
    }
}
