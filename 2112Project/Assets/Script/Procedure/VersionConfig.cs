using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 资源数据类，包含资源名称、大小和MD5值
/// </summary>
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
/// 资源清单类
/// </summary>
public class VersionConfig
{
<<<<<<< HEAD
    public const string ConfigFilePath = "Assets/Resources/versionconfig.json";
    public int VersionCode;
    public string Url;
    public List<ABData> listDatas = new List<ABData>();

    static string GetFileMD5(string filePath)
=======
    public const string ConfigFilePath = "Assets/Resources/versionconfig.json";  // 资源清单文件路径
    public const string VersionCodeFilePath = "Assets/Resources/versioncode.txt"; // 版本号存储文件路径
    public string VersionCode;  // 版本号（现在改为字符串）
    public string Url;
    public List<ABData> listDatas = new List<ABData>();

    /// <summary>
    /// 获取当前版本号
    /// </summary>
    public static string GetCurrentVersionCode()
>>>>>>> 795f0458aa55e7c23f5ea287f6335d9ad8b16de5
    {
        FileStream fs = new FileStream(filePath, FileMode.Open);
        MD5 md5 = MD5.Create();
        byte[] bytes = md5.ComputeHash(fs);
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < bytes.Length; i++)
        {
<<<<<<< HEAD
            sb.Append(bytes[i].ToString("x2"));
=======
            return File.ReadAllText(VersionCodeFilePath).Trim();
        }
        return "1.0.0"; // 默认初始版本号
    }

    /// <summary>
    /// 更新版本号并存储到文件
    /// </summary>
    public static void UpdateVersionCode(string newVersionCode)
    {
        Debug.Log(newVersionCode);
        File.WriteAllText(VersionCodeFilePath, newVersionCode);
    }

    /// <summary>
    /// 获取文件的大小和MD5值
    /// </summary>
    static (int, string) GetFileSizeAndMD5(string filePath)
    {
        using (FileStream fs = new FileStream(filePath, FileMode.Open))
        {
            MD5 md5 = MD5.Create();
            byte[] hashBytes = md5.ComputeHash(fs);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                sb.Append(b.ToString("x2"));
            }
            return ((int)fs.Length, sb.ToString());
>>>>>>> 795f0458aa55e7c23f5ea287f6335d9ad8b16de5
        }
        return sb.ToString();
    }

    [MenuItem("Tools/生成资源清单")]
    public static void MakeVersionConfig()
    {
<<<<<<< HEAD
        int currentVersionCode = EditorPrefs.GetInt("code", 0);
        VersionConfig vcf = new VersionConfig();
        vcf.Url = "http://10.161.16.41/Resources/";
        string[] files = Directory.GetFiles(Application.dataPath + "/Resources");
        // string[] files = Directory.GetFiles(Application.streamingAssetsPath);
        bool hasChange = false;
        for (int i = 0; i < files.Length; i++)
=======
        string currentVersionCode = GetCurrentVersionCode();  // 获取当前版本号
        VersionConfig vcf = new VersionConfig();
        vcf.Url = "http://10.161.16.41/Resources/";

        // 读取旧的版本配置文件
        VersionConfig oldConfig = null;
        if (File.Exists(VersionConfig.ConfigFilePath))
        {
            string oldJson = File.ReadAllText(VersionConfig.ConfigFilePath);
            oldConfig = JsonConvert.DeserializeObject<VersionConfig>(oldJson);
        }

        // 创建一个字典来查找旧的资源信息
        Dictionary<string, ABData> oldResourceDict = new Dictionary<string, ABData>();
        if (oldConfig != null)
        {
            foreach (var data in oldConfig.listDatas)
            {
                oldResourceDict[data.ABName] = data;
            }
        }

        string[] files = null;
        try
        {
            files = Directory.GetFiles(Application.dataPath + "/Resources");//生成资源清单路径
        }
        catch (Exception ex)
        {
            Debug.LogError($"获取资源文件时出现错误：{ex.Message}\n{ex.StackTrace}");
            return;
        }

        bool hasMajorChange = false;  // 标识是否有重大变化（如资源删除）
        bool hasMinorChange = false;  // 标识是否有小版本变化（如资源新增或修改）

        int addedCount = 0;   // 新增资源计数
        int modifiedCount = 0; // 修改资源计数
        int deletedCount = 0; // 删除资源计数

        HashSet<string> currentFiles = new HashSet<string>();  // 当前存在的文件

        // 并行处理文件
        Parallel.For(0, files.Length, i =>
>>>>>>> 795f0458aa55e7c23f5ea287f6335d9ad8b16de5
        {
            if (Path.GetExtension(files[i]) == ".u3d" || Path.GetExtension(files[i]) == ".meta")
            {
                string fileName = Path.GetFileName(files[i]);
<<<<<<< HEAD
                int len = File.ReadAllBytes(files[i]).Length;
                string md5 = GetFileMD5(files[i]);
                ABData aBData = new ABData(fileName, len, md5);
                if (!vcf.listDatas.Exists(data => data.ABName == fileName && data.ABbytes == len && data.Md5 == md5))
                {
                    hasChange = true;
=======
                (int fileSize, string md5) = GetFileSizeAndMD5(files[i]);

                ABData newABData = new ABData(fileName, fileSize, md5);

                lock (currentFiles)  // 确保线程安全
                {
                    currentFiles.Add(fileName);
                }

                lock (vcf.listDatas)  // 确保访问资源列表的线程安全
                {
                    if (oldResourceDict.ContainsKey(fileName))
                    {
                        var oldData = oldResourceDict[fileName];
                        if (oldData.ABbytes != fileSize || oldData.Md5 != md5)
                        {
                            hasMinorChange = true;  // 文件有变化，标记小版本变化
                            modifiedCount++;
                            vcf.listDatas.Add(newABData);
                        }
                        else
                        {
                            vcf.listDatas.Add(oldData);  // 保留旧数据
                        }
                    }
                    else
                    {
                        hasMinorChange = true;  // 新增资源，标记小版本变化
                        addedCount++;
                        vcf.listDatas.Add(newABData);
                    }
                }
            }
        });

        // 检测资源是否被删除
        if (oldConfig != null)
        {
            foreach (var oldData in oldConfig.listDatas)
            {
                if (!currentFiles.Contains(oldData.ABName))
                {
                    Debug.Log($"资源 \"{oldData.ABName}\" 被删除。");
                    hasMajorChange = true;  // 资源删除，标记重大变化
                    deletedCount++;
>>>>>>> 795f0458aa55e7c23f5ea287f6335d9ad8b16de5
                }
                vcf.listDatas.Add(aBData);
            }
        }
<<<<<<< HEAD
        if (hasChange)
        {
            vcf.VersionCode = currentVersionCode + 1;
            EditorPrefs.SetInt("code", vcf.VersionCode);
=======

        // 根据变化情况更新版本号
        string[] versionParts = currentVersionCode.Split('.');
        int major = int.Parse(versionParts[0]);
        int minor = int.Parse(versionParts[1]);
        int patch = int.Parse(versionParts[2]);

        // 如果有删除的资源，更新中版本号
        if (hasMajorChange)
        {
            vcf.VersionCode = $"{major}.{minor + 1}.0";  // 更新中版本号，重置修订号
        }
        else if (hasMinorChange)
        {
            // 增加或修改资源次数用于判断是否需要更新大版本
            int middleVersionUpdates = oldConfig != null ? int.Parse(versionParts[1]) : 0;

            if (middleVersionUpdates >= 10)
            {
                vcf.VersionCode = $"{major + 1}.0.0";  // 中版本更新次数超过10次，更新大版本
            }
            else
            {
                vcf.VersionCode = $"{major}.{minor}.{patch + 1}";  // 小版本变化，更新修订号
            }
>>>>>>> 795f0458aa55e7c23f5ea287f6335d9ad8b16de5
        }
        else
        {
            vcf.VersionCode = $"{major}.{minor}.{patch}";  // 无变化，保持原版本号
        }
<<<<<<< HEAD
        string json = JsonConvert.SerializeObject(vcf);
        File.WriteAllText(ConfigFilePath, json);
        AssetDatabase.Refresh();
=======

        // 保存新的资源清单
        string json = JsonConvert.SerializeObject(vcf, Formatting.Indented);
        try
        {
            File.WriteAllText(VersionConfig.ConfigFilePath, json);
            AssetDatabase.Refresh();
        }
        catch (Exception ex)
        {
            Debug.LogError($"保存资源清单时出现错误：{ex.Message}\n{ex.StackTrace}");
        }

        UpdateVersionCode(vcf.VersionCode);  // 更新版本号到文件
>>>>>>> 795f0458aa55e7c23f5ea287f6335d9ad8b16de5
    }

    /// <summary>
    /// 对比资源清单，检测是否有新增、修改或删除的资源
    /// </summary>
<<<<<<< HEAD
    /// <param name="oldListPath">客户端资源清单</param>
    /// <param name="newListPath">服务器资源清单</param>
=======
>>>>>>> 795f0458aa55e7c23f5ea287f6335d9ad8b16de5
    public static bool CompareResourceLists(string oldListPath, string newListPath)
    {
        bool isChange = false;
        string oldJson = File.ReadAllText(oldListPath);
        string newJson = File.ReadAllText(newListPath);

        VersionConfig oldConfig = JsonConvert.DeserializeObject<VersionConfig>(oldJson);
        VersionConfig newConfig = JsonConvert.DeserializeObject<VersionConfig>(newJson);

        Debug.Log($"客户端版本号: {oldConfig.VersionCode}");
        Debug.Log($"服务器版本号: {newConfig.VersionCode}");

<<<<<<< HEAD
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
=======
        // 创建两个字典方便对比
        Dictionary<string, ABData> oldResourceDict = new Dictionary<string, ABData>();
        foreach (var data in oldConfig.listDatas)
        {
            oldResourceDict[data.ABName] = data;
        }

        Dictionary<string, ABData> newResourceDict = new Dictionary<string, ABData>();
        foreach (var data in newConfig.listDatas)
        {
            newResourceDict[data.ABName] = data;
        }

        // 检测新增或修改的资源
        foreach (var newData in newResourceDict)
        {
            if (!oldResourceDict.ContainsKey(newData.Key))
            {
                Debug.Log($"新增资源: {newData.Key}");
                isChanged = true;
            }
            else
            {
                if (oldResourceDict[newData.Key].Md5 != newData.Value.Md5)
                {
                    Debug.Log($"修改资源: {newData.Key}");
                    isChanged = true;
                }
            }
        }

        // 检测删除的资源
        foreach (var oldData in oldResourceDict)
>>>>>>> 795f0458aa55e7c23f5ea287f6335d9ad8b16de5
        {
            if (!newConfig.listDatas.Exists(data => data.ABName == oldData.Key))
            {
<<<<<<< HEAD
                Debug.Log($"资源“ {oldData.Key}” 已被移除。");
                isChange = true;
=======
                Debug.Log($"删除资源: {oldData.Key}");
                isChanged = true;
>>>>>>> 795f0458aa55e7c23f5ea287f6335d9ad8b16de5
            }
        }
        return isChange;
    }
}