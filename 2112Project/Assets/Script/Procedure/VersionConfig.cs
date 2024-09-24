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
/// ��Դ�嵥
/// </summary>
public class VersionConfig
{
    public const string ConfigFilePath = "Assets/Resources/versionconfig.json";
    public const string VersionCodeFilePath = "Assets/Resources/versioncode.txt"; // �洢�汾�ŵ��ļ�·��
    public int VersionCode; // �汾�Ŷ���
    public string Url;
    public List<ABData> listDatas = new List<ABData>();

    /// <summary>
    /// ��ȡ��ǰ�汾��
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
    /// ���°汾��
    /// </summary>
    public static void UpdateVersionCode(int newVersionCode)
    {
        File.WriteAllText(VersionCodeFilePath, newVersionCode.ToString());
    }

    /// <summary>
    /// ��ȡ�ļ��Ĵ�С��MD5
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
    /// ֧�ֵ��ļ���չ���б�
    /// </summary>
    private static readonly HashSet<string> SupportedExtensions = new HashSet<string> { ".u3d", ".meta" };

    /// <summary>
    /// �ж��ļ��Ƿ�Ϊ֧�ֵ�����
    /// </summary>
    static bool IsSupportedFile(string filePath)
    {
        return SupportedExtensions.Contains(Path.GetExtension(filePath).ToLower());
    }

    /// <summary>
    /// ������Դ�嵥
    /// </summary>
    [MenuItem("Tools/������Դ�嵥")]
    public static void MakeVersionConfig()
    {
        int currentVersionCode = GetCurrentVersionCode();
        VersionConfig vcf = new VersionConfig();
        vcf.Url = "http://10.161.16.41/Resources/";

        // ���Զ�ȡ�ɵİ汾�����ļ�
        VersionConfig oldConfig = null;
        if (File.Exists(VersionConfig.ConfigFilePath))
        {
            string oldJson = File.ReadAllText(VersionConfig.ConfigFilePath);
            oldConfig = JsonConvert.DeserializeObject<VersionConfig>(oldJson);
        }

        // �����ֵ䣬�������ٲ��Ҿɵ���Դ��Ϣ
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
            Debug.LogError($"��ȡ��Դ�ļ�ʱ���ִ���{ex.Message}\n{ex.StackTrace}");
            return;
        }

        bool hasChange = false;
        HashSet<string> currentFiles = new HashSet<string>(); // ��ǰ���ڵ��ļ�

        // ���д����ļ�
        Parallel.For(0, files.Length, i =>
        {
            if (IsSupportedFile(files[i]))
            {
                string fileName = Path.GetFileName(files[i]);
                (int fileSize, string md5) = GetFileSizeAndMD5(files[i]);

                ABData newABData = new ABData(fileName, fileSize, md5);

                lock (currentFiles)  // ȷ���̰߳�ȫ
                {
                    currentFiles.Add(fileName);
                }

                lock (vcf.listDatas)  // ȷ��������Դ�б���̰߳�ȫ
                {
                    if (oldResourceDict.ContainsKey(fileName) && oldResourceDict[fileName].ABbytes == fileSize && oldResourceDict[fileName].Md5 == md5)
                    {
                        vcf.listDatas.Add(oldResourceDict[fileName]);  // ����������
                    }
                    else
                    {
                        hasChange = true;
                        vcf.listDatas.Add(newABData);  // ���������
                    }
                }
            }
        });

        // ���ɾ������Դ
        if (oldConfig != null)
        {
            foreach (var oldData in oldConfig.listDatas)
            {
                if (!currentFiles.Contains(oldData.ABName))
                {
                    Debug.Log($"��Դ \"{oldData.ABName}\" ��ɾ����");
                    hasChange = true;  // ��Դ��ɾ�������°汾
                }
            }
        }

        // �汾�Ÿ����߼�
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

        // �����µ���Դ�嵥
        string json = JsonConvert.SerializeObject(vcf, Formatting.Indented);
        try
        {
            File.WriteAllText(VersionConfig.ConfigFilePath, json);
            AssetDatabase.Refresh();
        }
        catch (Exception ex)
        {
            Debug.LogError($"������Դ�嵥ʱ���ִ���{ex.Message}\n{ex.StackTrace}");
        }
    }
    /// <summary>
    /// �Ա���Դ�嵥
    /// </summary>
    /// <param name="oldListPath">������Դ�嵥</param>
    /// <param name="newListPath">��������Դ�嵥</param>
    public static bool CompareResourceLists(string oldListPath, string newListPath)
    {
        bool isChanged = false;
        string oldJson = File.ReadAllText(oldListPath);
        string newJson = File.ReadAllText(newListPath);

        VersionConfig oldConfig = JsonConvert.DeserializeObject<VersionConfig>(oldJson);
        VersionConfig newConfig = JsonConvert.DeserializeObject<VersionConfig>(newJson);

        Debug.Log($"���ذ汾��: {oldConfig.VersionCode}");
        Debug.Log($"�������汾��: {newConfig.VersionCode}");

        // ���������ֵ䷽��Ա�
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

        // �Ա���Դ������������޸ĵ���Դ
        foreach (var newData in newConfig.listDatas)
        {
            if (oldResourceDict.ContainsKey(newData.ABName))
            {
                var oldData = oldResourceDict[newData.ABName];
                if (oldData.ABbytes != newData.ABbytes || oldData.Md5 != newData.Md5)
                {
                    Debug.Log($"��Դ \"{newData.ABName}\" �ѱ��޸ġ�");
                    Debug.Log($"�ɴ�С: {oldData.ABbytes}, �´�С: {newData.ABbytes}");
                    Debug.Log($"��MD5: {oldData.Md5}, ��MD5: {newData.Md5}");
                    isChanged = true;
                }
            }
            else
            {
                Debug.Log($"��Դ \"{newData.ABName}\" Ϊ������Դ��");
                isChanged = true;
            }
        }

        // ��ⱻɾ������Դ
        foreach (var oldData in oldResourceDict)
        {
            if (!newResourceDict.ContainsKey(oldData.Key))
            {
                Debug.Log($"��Դ \"{oldData.Key}\" ��ɾ����");
                isChanged = true;
            }
        }

        return isChanged;
    }
}
