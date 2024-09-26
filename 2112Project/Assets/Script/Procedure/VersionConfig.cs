using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
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
<<<<<<< HEAD
    public const string ConfigFilePath = "Assets/Resources/versionconfig.json";
    public int VersionCode;
    public string Url;
    public List<ABData> listDatas = new List<ABData>();

    static string GetFileMD5(string filePath)
=======
    public const string ConfigFilePath = "Assets/Resources/versionconfig.json";  // ��Դ�嵥�ļ�·��
    public const string VersionCodeFilePath = "Assets/Resources/versioncode.txt"; // �汾�Ŵ洢�ļ�·��
    public string VersionCode;  // �汾�ţ����ڸ�Ϊ�ַ�����
    public string Url;
    public List<ABData> listDatas = new List<ABData>();

    /// <summary>
    /// ��ȡ��ǰ�汾��
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
>>>>>>> 795f0458aa55e7c23f5ea287f6335d9ad8b16de5
        }
        return sb.ToString();
    }

    [MenuItem("Tools/������Դ�嵥")]
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
>>>>>>> 795f0458aa55e7c23f5ea287f6335d9ad8b16de5
        }
        else
        {
            vcf.VersionCode = $"{major}.{minor}.{patch}";  // �ޱ仯������ԭ�汾��
        }
<<<<<<< HEAD
        string json = JsonConvert.SerializeObject(vcf);
        File.WriteAllText(ConfigFilePath, json);
        AssetDatabase.Refresh();
=======

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
>>>>>>> 795f0458aa55e7c23f5ea287f6335d9ad8b16de5
    }

    /// <summary>
    /// �Ա���Դ�嵥������Ƿ����������޸Ļ�ɾ������Դ
    /// </summary>
<<<<<<< HEAD
    /// <param name="oldListPath">�ͻ�����Դ�嵥</param>
    /// <param name="newListPath">��������Դ�嵥</param>
=======
>>>>>>> 795f0458aa55e7c23f5ea287f6335d9ad8b16de5
    public static bool CompareResourceLists(string oldListPath, string newListPath)
    {
        bool isChange = false;
        string oldJson = File.ReadAllText(oldListPath);
        string newJson = File.ReadAllText(newListPath);

        VersionConfig oldConfig = JsonConvert.DeserializeObject<VersionConfig>(oldJson);
        VersionConfig newConfig = JsonConvert.DeserializeObject<VersionConfig>(newJson);

        Debug.Log($"�ͻ��˰汾��: {oldConfig.VersionCode}");
        Debug.Log($"�������汾��: {newConfig.VersionCode}");

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
                    Debug.Log($"��Դ�� {newData.ABName} �������ı䣡");
                    Debug.Log($"Old Size: {oldData.ABbytes}, New Size: {newData.ABbytes}");
                    Debug.Log($"Old MD5: {oldData.Md5}, New MD5: {newData.Md5}");
                    isChange = true;
                }
            }
            else
            {
                Debug.Log($"��Դ ��{newData.ABName}�� δ�����ı䡣");
            }
        }
        //�Ƿ�ɾ��
        foreach (var oldData in oldResources)
=======
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
>>>>>>> 795f0458aa55e7c23f5ea287f6335d9ad8b16de5
        {
            if (!newConfig.listDatas.Exists(data => data.ABName == oldData.Key))
            {
<<<<<<< HEAD
                Debug.Log($"��Դ�� {oldData.Key}�� �ѱ��Ƴ���");
                isChange = true;
=======
                Debug.Log($"ɾ����Դ: {oldData.Key}");
                isChanged = true;
>>>>>>> 795f0458aa55e7c23f5ea287f6335d9ad8b16de5
            }
        }
        return isChange;
    }
}