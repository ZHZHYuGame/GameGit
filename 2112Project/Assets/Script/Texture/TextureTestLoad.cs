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
        //    Debug.LogError("纹理文件路径为空");
        //    yield break;
        //}

        //// 使用Path.Combine方法确保路径的正确性
        //string fullPath = Path.Combine(Application.streamingAssetsPath, filePath);

        //if (!File.Exists(fullPath))
        //{
        //    Debug.LogError("Texture file does not exist: " + fullPath);
        //    yield break;
        //}

        //// 使用UnityWebRequest类加载纹理
        //UnityWebRequest www = UnityWebRequestTexture.GetTexture(fullPath);
        //yield return www.SendWebRequest();

        //if (www.result== UnityWebRequest.Result.ConnectionError || www.result== UnityWebRequest.Result.ProtocolError)
        //{
        //    Debug.LogError("加载纹理错误！！！（网络错误或者http地址错误）" + www.error);
        //}
        //else
        //{
        //    texture = DownloadHandlerTexture.GetContent(www);
        //    transform.GetComponent<MeshRenderer>().material.mainTexture = texture;
        //}

        // 使用UnityWebRequest类加载纹理
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(filePath);
        yield return www.SendWebRequest();

        //下载完成
        if (www.isDone)
        {
            texture = DownloadHandlerTexture.GetContent(www);
            transform.GetComponent<MeshRenderer>().material.mainTexture = texture;
        }
    }
}
