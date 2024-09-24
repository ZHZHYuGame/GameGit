using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
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
    public const string VersionCodeFilePath = "Assets/Resources/versioncode.txt"; // 存储版本号的文件路径
    public int VersionCode; // 版本号定义
    public string Url;
    public List<ABData> listDatas = new List<ABData>();

    /// <summary>
    /// 获取当前版本号
    /// </summary>
    public static int GetCurrentVersionCode()
    {
        if (File.Exists(VersionCodeFilePath))
        {
            return int.Parse(File.ReadAllText(VersionCodeFilePath));
        }
        return 0;
    }

    /// <summary>
    /// 更新版本号
    /// </summary>
    public static void UpdateVersionCode(int newVersionCode)
    {
        File.WriteAllText(VersionCodeFilePath, newVersionCode.ToString());
    }

    /// <summary>
    /// 获取文件的大小和MD5
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
        }
    }

    /// <summary>
    /// 支持的文件扩展名列表
    /// </summary>
    private static readonly HashSet<string> SupportedExtensions = new HashSet<string> { ".u3d", ".meta" };

    /// <summary>
    /// 判断文件是否为支持的类型
    /// </summary>
    static bool IsSupportedFile(string filePath)
    {
        return SupportedExtensions.Contains(Path.GetExtension(filePath).ToLower());
    }

    /// <summary>
    /// 生成资源清单
    /// </summary>
    [MenuItem("Tools/生成资源清单")]
    public static void MakeVersionConfig()
    {
        int currentVersionCode = GetCurrentVersionCode();
        VersionConfig vcf = new VersionConfig();
        vcf.Url = "http://10.161.16.41/Resources/";

        // 尝试读取旧的版本配置文件
        VersionConfig oldConfig = null;
        if (File.Exists(VersionConfig.ConfigFilePath))
        {
            string oldJson = File.ReadAllText(VersionConfig.ConfigFilePath);
            oldConfig = JsonConvert.DeserializeObject<VersionConfig>(oldJson);
        }

        // 创建字典，用来快速查找旧的资源信息
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
            files = Directory.GetFiles(Application.dataPath + "/Resources");
        }
        catch (Exception ex)
        {
            Debug.LogError($"获取资源文件时出现错误：{ex.Message}\n{ex.StackTrace}");
            return;
        }

        bool hasChange = false;
        HashSet<string> currentFiles = new HashSet<string>(); // 当前存在的文件

        // 并行处理文件
        Parallel.For(0, files.Length, i =>
        {
            if (IsSupportedFile(files[i]))
            {
                string fileName = Path.GetFileName(files[i]);
                (int fileSize, string md5) = GetFileSizeAndMD5(files[i]);

                ABData newABData = new ABData(fileName, fileSize, md5);

                lock (currentFiles)  // 确保线程安全
                {
                    currentFiles.Add(fileName);
                }

                lock (vcf.listDatas)  // 确保访问资源列表的线程安全
                {
                    if (oldResourceDict.ContainsKey(fileName) && oldResourceDict[fileName].ABbytes == fileSize && oldResourceDict[fileName].Md5 == md5)
                    {
                        vcf.listDatas.Add(oldResourceDict[fileName]);  // 保留旧数据
                    }
                    else
                    {
                        hasChange = true;
                        vcf.listDatas.Add(newABData);  // 添加新数据
                    }
                }
            }
        });

        // 检测删除的资源
        if (oldConfig != null)
        {
            foreach (var oldData in oldConfig.listDatas)
            {
                if (!currentFiles.Contains(oldData.ABName))
                {
                    Debug.Log($"资源 \"{oldData.ABName}\" 被删除。");
                    hasChange = true;  // 资源被删除，更新版本
                }
            }
        }

        // 版本号更新逻辑
        if (hasChange)
        {
            vcf.VersionCode = currentVersionCode + 1;
            UpdateVersionCode(vcf.VersionCode);
        }
        else if (oldConfig != null)
        {
            vcf.VersionCode = oldConfig.VersionCode;
        }
        else
        {
            vcf.VersionCode = currentVersionCode;
        }

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
    }
    /// <summary>
    /// 对比资源清单
    /// </summary>
    /// <param name="oldListPath">本地资源清单</param>
    /// <param name="newListPath">服务器资源清单</param>
    public static bool CompareResourceLists(string oldListPath, string newListPath)
    {
        bool isChanged = false;
        string oldJson = File.ReadAllText(oldListPath);
        string newJson = File.ReadAllText(newListPath);

        VersionConfig oldConfig = JsonConvert.DeserializeObject<VersionConfig>(oldJson);
        VersionConfig newConfig = JsonConvert.DeserializeObject<VersionConfig>(newJson);

        Debug.Log($"本地版本号: {oldConfig.VersionCode}");
        Debug.Log($"服务器版本号: {newConfig.VersionCode}");

        // 创建两个字典方便对比
        Dictionary<string, ABData> oldResourceDict = new Dictionary<string, ABData>();
        Dictionary<string, ABData> newResourceDict = new Dictionary<string, ABData>();

        foreach (var data in oldConfig.listDatas)
        {
            oldResourceDict[data.ABName] = data;
        }

        foreach (var data in newConfig.listDatas)
        {
            newResourceDict[data.ABName] = data;
        }

        // 对比资源：检测新增或修改的资源
        foreach (var newData in newConfig.listDatas)
        {
            if (oldResourceDict.ContainsKey(newData.ABName))
            {
                var oldData = oldResourceDict[newData.ABName];
                if (oldData.ABbytes != newData.ABbytes || oldData.Md5 != newData.Md5)
                {
                    Debug.Log($"资源 \"{newData.ABName}\" 已被修改。");
                    Debug.Log($"旧大小: {oldData.ABbytes}, 新大小: {newData.ABbytes}");
                    Debug.Log($"旧MD5: {oldData.Md5}, 新MD5: {newData.Md5}");
                    isChanged = true;
                }
            }
            else
            {
                Debug.Log($"资源 \"{newData.ABName}\" 为新增资源。");
                isChanged = true;
            }
        }

        // 检测被删除的资源
        foreach (var oldData in oldResourceDict)
        {
            if (!newResourceDict.ContainsKey(oldData.Key))
            {
                Debug.Log($"资源 \"{oldData.Key}\" 被删除。");
                isChanged = true;
            }
        }

        return isChanged;
    }
}
