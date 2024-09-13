using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEditor;
using UnityEngine;

public class ABData
{
    public string ABName;
    public int ABbytes;
    public string Md5;

    public ABData(string aBName, int aBbytes, string md5)
    {
        ABName = aBName;
        ABbytes = aBbytes;
        Md5 = md5;
    }
}
/// <summary>
/// 资源清单
/// </summary>
public class VersionConfig
{
    public const string ConfigFilePath = "Assets/Resources/versionconfig.json";
    public int VersionCode;
    public string Url;
    public List<ABData> listDatas = new List<ABData>();

    static string GetFileMD5(string filePath)
    {
        FileStream fs = new FileStream(filePath, FileMode.Open);
        MD5 md5 = MD5.Create();
        byte[] bytes = md5.ComputeHash(fs);
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < bytes.Length; i++)
        {
            sb.Append(bytes[i].ToString("x2"));
        }
        return sb.ToString();
    }

    [MenuItem("Tools/生成资源清单")]
    public static void MakeVersionConfig()
    {
        int currentVersionCode = EditorPrefs.GetInt("code", 0);
        VersionConfig vcf = new VersionConfig();
        vcf.Url = "http://10.161.16.41/Resources/";
        string[] files = Directory.GetFiles(Application.dataPath + "/Resources");
        // string[] files = Directory.GetFiles(Application.streamingAssetsPath);
        bool hasChange = false;
        for (int i = 0; i < files.Length; i++)
        {
            if (Path.GetExtension(files[i]) == ".u3d" || Path.GetExtension(files[i]) == ".meta")
            {
                string fileName = Path.GetFileName(files[i]);
                int len = File.ReadAllBytes(files[i]).Length;
                string md5 = GetFileMD5(files[i]);
                ABData aBData = new ABData(fileName, len, md5);
                if (!vcf.listDatas.Exists(data => data.ABName == fileName && data.ABbytes == len && data.Md5 == md5))
                {
                    hasChange = true;
                }
                vcf.listDatas.Add(aBData);
            }
        }
        if (hasChange)
        {
            vcf.VersionCode = currentVersionCode + 1;
            EditorPrefs.SetInt("code", vcf.VersionCode);
        }
        else
        {
            vcf.VersionCode = currentVersionCode;
        }
        string json = JsonConvert.SerializeObject(vcf);
        File.WriteAllText(ConfigFilePath, json);
        AssetDatabase.Refresh();
    }
    /// <summary>
    /// 对比资源清单
    /// </summary>
    /// <param name="oldListPath">客户端资源清单</param>
    /// <param name="newListPath">服务器资源清单</param>
    public static bool CompareResourceLists(string oldListPath, string newListPath)
    {
        bool isChange = false;
        string oldJson = File.ReadAllText(oldListPath);
        string newJson = File.ReadAllText(newListPath);

        VersionConfig oldConfig = JsonConvert.DeserializeObject<VersionConfig>(oldJson);
        VersionConfig newConfig = JsonConvert.DeserializeObject<VersionConfig>(newJson);

        Debug.Log($"客户端版本号: {oldConfig.VersionCode}");
        Debug.Log($"服务器版本号: {newConfig.VersionCode}");

        Dictionary<string, ABData> oldResources = new Dictionary<string, ABData>();
        foreach (var data in oldConfig.listDatas)
        {
            oldResources[data.ABName] = data;
        }

        foreach (var newData in newConfig.listDatas)
        {
            if (oldResources.ContainsKey(newData.ABName))
            {
                var oldData = oldResources[newData.ABName];
                if (oldData.ABbytes != newData.ABbytes || oldData.Md5 != newData.Md5)
                {
                    Debug.Log($"资源“ {newData.ABName} ”发生改变！");
                    Debug.Log($"Old Size: {oldData.ABbytes}, New Size: {newData.ABbytes}");
                    Debug.Log($"Old MD5: {oldData.Md5}, New MD5: {newData.Md5}");
                    isChange = true;
                }
            }
            else
            {
                Debug.Log($"资源 “{newData.ABName}” 未发生改变。");
            }
        }
        //是否删除
        foreach (var oldData in oldResources)
        {
            if (!newConfig.listDatas.Exists(data => data.ABName == oldData.Key))
            {
                Debug.Log($"资源“ {oldData.Key}” 已被移除。");
                isChange = true;
            }
        }
        return isChange;
    }
}