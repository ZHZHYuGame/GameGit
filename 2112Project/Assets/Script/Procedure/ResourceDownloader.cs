using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class ResourceDownloader : MonoBehaviour
{
   // 版本信息文件的URL
    public string versionFileUrl = "";
    // 资源的基础URL
    public string resourceBaseUrl = "http://10.161.16.41/Resources/";
    // 本地版本信息文件的路径
    public string localVersionFilePath = "";
    // 资源下载存储目录
    public string downloadDirectory = "Assets/Resources/";

    // 存储远程版本信息的字典
    private Dictionary<string, string> remoteVersionInfo;

    void Start()
    {
        // 启动检查并更新资源的协程
        StartCoroutine(CheckAndUpdateResources());
    }

    // 检查并更新资源的协程方法
    private IEnumerator CheckAndUpdateResources()
    {
        // 下载远程版本信息
        yield return StartCoroutine(DownloadVersionInfo());
        // 读取本地版本
        string localVersion = ReadLocalVersion();
        
        // 遍历远程版本信息字典
        foreach (var item in remoteVersionInfo)
        {
            string resourceName = item.Key;  // 资源名称
            string remoteVersion = item.Value;  // 远程版本号

            // 如果本地版本和远程版本不同
            if (localVersion != remoteVersion)
            {
                // 下载新版本资源
                yield return StartCoroutine(DownloadResource(resourceName));
                // 更新本地版本信息
                UpdateLocalVersion(remoteVersion);
            }
        }
    }

    // 下载版本信息的协程方法
    private IEnumerator DownloadVersionInfo()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(versionFileUrl))
        {
            // 发送请求并等待响应
            yield return www.SendWebRequest();

            // 检查请求是否成功
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"下载版本信息失败: {www.error}");
                yield break; // 失败则退出协程
            }

            // 解析下载的JSON数据
            string json = www.downloadHandler.text;
            remoteVersionInfo = JsonUtility.FromJson<VersionInfo>(json).resources;
        }
    }

    // 下载资源的协程方法
    private IEnumerator DownloadResource(string resourceName)
    {
        // 资源的完整URL
        string url = resourceBaseUrl + resourceName;
        // 资源保存的本地路径
        string filePath = Path.Combine(downloadDirectory, resourceName);

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            // 设置下载处理器以将文件保存到指定路径
            www.downloadHandler = new DownloadHandlerFile(filePath);
            // 发送请求并等待响应
            yield return www.SendWebRequest();

            // 检查请求是否成功
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"下载资源失败: {www.error}");
            }
        }
    }

    // 读取本地版本信息的方法
    private string ReadLocalVersion()
    {
        // 检查版本信息文件是否存在
        if (File.Exists(localVersionFilePath))
        {
            // 读取并返回文件内容（版本号）
            return File.ReadAllText(localVersionFilePath).Trim();
        }
        return string.Empty; // 文件不存在时返回空字符串
    }

    // 更新本地版本信息的方法
    private void UpdateLocalVersion(string newVersion)
    {
        // 将新的版本号写入本地版本信息文件
        File.WriteAllText(localVersionFilePath, newVersion);
    }

    // 用于解析版本信息的JSON数据类
    [System.Serializable]
    private class VersionInfo
    {
        public Dictionary<string, string> resources; // 资源名称和版本号的字典
    }
}
