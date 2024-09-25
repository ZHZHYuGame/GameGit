using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

/// <summary>
/// ��Դ�����࣬������Դ���ơ���С��MD5ֵ
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
/// ��Դ�嵥��
/// </summary>
public class VersionConfig
{
    public const string ConfigFilePath = "Assets/Resources/versionconfig.json";  // ��Դ�嵥�ļ�·��
    public const string VersionCodeFilePath = "Assets/Resources/versioncode.txt"; // �汾�Ŵ洢�ļ�·��
    public string VersionCode;  // �汾�ţ����ڸ�Ϊ�ַ�����
    public string Url;
    public List<ABData> listDatas = new List<ABData>();

    /// <summary>
    /// ��ȡ��ǰ�汾��
    /// </summary>
    public static string GetCurrentVersionCode()
    {
        if (File.Exists(VersionCodeFilePath))
        {
            return File.ReadAllText(VersionCodeFilePath).Trim();
        }
        return "1.0.0"; // Ĭ�ϳ�ʼ�汾��
    }

    /// <summary>
    /// ���°汾�Ų��洢���ļ�
    /// </summary>
    public static void UpdateVersionCode(string newVersionCode)
    {
        Debug.Log(newVersionCode);
        File.WriteAllText(VersionCodeFilePath, newVersionCode);
    }

    /// <summary>
    /// ��ȡ�ļ��Ĵ�С��MD5ֵ
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
        string currentVersionCode = GetCurrentVersionCode();  // ��ȡ��ǰ�汾��
        VersionConfig vcf = new VersionConfig();
        vcf.Url = "http://10.161.16.41/Resources/";

        // ��ȡ�ɵİ汾�����ļ�
        VersionConfig oldConfig = null;
        if (File.Exists(VersionConfig.ConfigFilePath))
        {
            string oldJson = File.ReadAllText(VersionConfig.ConfigFilePath);
            oldConfig = JsonConvert.DeserializeObject<VersionConfig>(oldJson);
        }

        // ����һ���ֵ������Ҿɵ���Դ��Ϣ
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
            files = Directory.GetFiles(Application.dataPath + "/Resources");//������Դ�嵥·��
        }
        catch (Exception ex)
        {
            Debug.LogError($"��ȡ��Դ�ļ�ʱ���ִ���{ex.Message}\n{ex.StackTrace}");
            return;
        }

        bool hasMajorChange = false;  // ��ʶ�Ƿ����ش�仯������Դɾ����
        bool hasMinorChange = false;  // ��ʶ�Ƿ���С�汾�仯������Դ�������޸ģ�

        int addedCount = 0;   // ������Դ����
        int modifiedCount = 0; // �޸���Դ����
        int deletedCount = 0; // ɾ����Դ����

        HashSet<string> currentFiles = new HashSet<string>();  // ��ǰ���ڵ��ļ�

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
                    if (oldResourceDict.ContainsKey(fileName))
                    {
                        var oldData = oldResourceDict[fileName];
                        if (oldData.ABbytes != fileSize || oldData.Md5 != md5)
                        {
                            hasMinorChange = true;  // �ļ��б仯�����С�汾�仯
                            modifiedCount++;
                            vcf.listDatas.Add(newABData);
                        }
                        else
                        {
                            vcf.listDatas.Add(oldData);  // ����������
                        }
                    }
                    else
                    {
                        hasMinorChange = true;  // ������Դ�����С�汾�仯
                        addedCount++;
                        vcf.listDatas.Add(newABData);
                    }
                }
            }
        });

        // �����Դ�Ƿ�ɾ��
        if (oldConfig != null)
        {
            foreach (var oldData in oldConfig.listDatas)
            {
                if (!currentFiles.Contains(oldData.ABName))
                {
                    Debug.Log($"��Դ \"{oldData.ABName}\" ��ɾ����");
                    hasMajorChange = true;  // ��Դɾ��������ش�仯
                    deletedCount++;
                }
            }
        }

        // ���ݱ仯������°汾��
        string[] versionParts = currentVersionCode.Split('.');
        int major = int.Parse(versionParts[0]);
        int minor = int.Parse(versionParts[1]);
        int patch = int.Parse(versionParts[2]);

        // �����ɾ������Դ�������а汾��
        if (hasMajorChange)
        {
            vcf.VersionCode = $"{major}.{minor + 1}.0";  // �����а汾�ţ������޶���
        }
        else if (hasMinorChange)
        {
            // ���ӻ��޸���Դ���������ж��Ƿ���Ҫ���´�汾
            int middleVersionUpdates = oldConfig != null ? int.Parse(versionParts[1]) : 0;

            if (middleVersionUpdates >= 10)
            {
                vcf.VersionCode = $"{major + 1}.0.0";  // �а汾���´�������10�Σ����´�汾
            }
            else
            {
                vcf.VersionCode = $"{major}.{minor}.{patch + 1}";  // С�汾�仯�������޶���
            }
        }
        else
        {
            vcf.VersionCode = $"{major}.{minor}.{patch}";  // �ޱ仯������ԭ�汾��
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

        UpdateVersionCode(vcf.VersionCode);  // ���°汾�ŵ��ļ�
    }

    /// <summary>
    /// �Ա���Դ�嵥������Ƿ����������޸Ļ�ɾ������Դ
    /// </summary>
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
        foreach (var data in oldConfig.listDatas)
        {
            oldResourceDict[data.ABName] = data;
        }

        Dictionary<string, ABData> newResourceDict = new Dictionary<string, ABData>();
        foreach (var data in newConfig.listDatas)
        {
            newResourceDict[data.ABName] = data;
        }

        // ����������޸ĵ���Դ
        foreach (var newData in newResourceDict)
        {
            if (!oldResourceDict.ContainsKey(newData.Key))
            {
                Debug.Log($"������Դ: {newData.Key}");
                isChanged = true;
            }
            else
            {
                if (oldResourceDict[newData.Key].Md5 != newData.Value.Md5)
                {
                    Debug.Log($"�޸���Դ: {newData.Key}");
                    isChanged = true;
                }
            }
        }

        // ���ɾ������Դ
        foreach (var oldData in oldResourceDict)
        {
            if (!newResourceDict.ContainsKey(oldData.Key))
            {
                Debug.Log($"ɾ����Դ: {oldData.Key}");
                isChanged = true;
            }
        }

        return isChanged;
    }
}
