using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class TextureTestLoad : MonoBehaviour
{
    string filePath = "http://10.161.16.83/ccc/001.png";
    Texture texture;
    void Start()
    {
        TextureMgr.Ins.Init();
        StartCoroutine(LoadTexture());
    }
    IEnumerator LoadTexture()
    {
        //if (string.IsNullOrEmpty(filePath))
        //{
        //    Debug.Log(filePath);
        //    Debug.LogError("�����ļ�·��Ϊ��");
        //    yield break;
        //}

        //// ʹ��Path.Combine����ȷ��·������ȷ��
        //string fullPath = Path.Combine(Application.streamingAssetsPath, filePath);

        //if (!File.Exists(fullPath))
        //{
        //    Debug.LogError("Texture file does not exist: " + fullPath);
        //    yield break;
        //}

        //// ʹ��UnityWebRequest���������
        //UnityWebRequest www = UnityWebRequestTexture.GetTexture(fullPath);
        //yield return www.SendWebRequest();

        //if (www.result== UnityWebRequest.Result.ConnectionError || www.result== UnityWebRequest.Result.ProtocolError)
        //{
        //    Debug.LogError("����������󣡣���������������http��ַ����" + www.error);
        //}
        //else
        //{
        //    texture = DownloadHandlerTexture.GetContent(www);
        //    transform.GetComponent<MeshRenderer>().material.mainTexture = texture;
        //}

        // ʹ��UnityWebRequest���������
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(filePath);
        yield return www.SendWebRequest();

        //�������
        if (www.isDone)
        {
            texture = DownloadHandlerTexture.GetContent(www);
            transform.GetComponent<MeshRenderer>().material.mainTexture = texture;
        }
    }
}
