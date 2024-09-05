using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 内部事件消息的测试：进行派发消息
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
            string data = "账号：111，密码：222";
            int data2 = 111;
            Debug.Log("Login 登录发送消息: " + data2);
            GameManager.eventMgr.Dispatch(MessageType.Login, data2);
        }
    }
}
