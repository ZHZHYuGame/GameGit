using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ڲ��¼���Ϣ�Ĳ��ԣ������ɷ���Ϣ
/// </summary>
public class Login : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            string data = "�˺ţ�111�����룺222";
            int data2 = 111;
            Debug.Log("Login ��¼������Ϣ: " + data2);
            GameManager.eventMgr.Dispatch(MessageType.Login, data2);
        }
    }
}
