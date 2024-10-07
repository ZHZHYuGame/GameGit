using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

using UnityEngine.Networking;


public class AssetBundleManager : Singleton<AssetBundleManager>
{   
    protected string m_ABConfigABName = "assetbundleconfig";
   
    //资源关系依赖配表，可以根据crc来找到对应资源块
    protected Dictionary<uint, ResouceItem> m_ResouceItemDic = new Dictionary<uint, ResouceItem>();

    //储存已加载的AB包，key为crc
    protected Dictionary<uint, AssetBundleItem> m_AssetBundleItemDic = new Dictionary<uint, AssetBundleItem>();
   
    //AssetBundleItem类对象池
    protected ClassObjectPool<AssetBundleItem> m_AssetBundleItemPool = ObjectManager.Instance.GetOrCreatClassPool<AssetBundleItem>(500);

    public string ABLoadPath
    {
        get
        {
#if UNITY_ANDROID
            return Application.persistentDataPath + "/Origin/";
#else
            return Application.streamingAssetsPath + "/";
#endif
        }
    }
    public string AesKey =ConStr.AesKey;

    //public uint crc = 0;
    /// <summary>
    /// 加载ab配置表
    /// </summary>
    /// <returns></returns>
    public bool LoadAssetBundleConfig()
    {
#if UNITY_EDITOR
        if (!ResourceManager.Instance.m_LoadFormAssetBundle)
            return false;
#endif
        m_ResouceItemDic.Clear();
        string configPath = ABLoadPath + m_ABConfigABName;

        string hotPath = HotPatchManager.Instance.ComputeABPath(m_ABConfigABName);
        configPath = string.IsNullOrEmpty(hotPath) ? configPath : hotPath;

       // string tempPath = Application.streamingAssetsPath + "/" + m_ABConfigABName;
       // byte[] bytes = AES.AESFileByteDecrypt(tempPath, AesKey);
        byte[] bytes = AES.AESFileByteDecrypt(configPath, AesKey);

        //原AB从文件夹加载，现从加密后的AB加载
        //AssetBundle configAB = AssetBundle.LoadFromFile(configPath);
        AssetBundle configAB = AssetBundle.LoadFromMemory(bytes);
       
        TextAsset textAsset = configAB.LoadAsset<TextAsset>(m_ABConfigABName);
        if (textAsset == null)
        {
            Debug.LogError("AssetBundleConfig is no exist!");
            return false;
        }

        AssetBundleConfig config = null;
        using (MemoryStream stream = new MemoryStream(textAsset.bytes))
        {
            BinaryFormatter bf = new BinaryFormatter();
            config = (AssetBundleConfig)bf.Deserialize(stream);
        }

        for (int i = 0; i < config.ABList.Count; i++)
        {
            ABBase abBase = config.ABList[i];
            ResouceItem item = new ResouceItem();
            item.m_Crc = abBase.Crc;
            item.m_AssetName = abBase.AssetName;
            item.m_ABName = abBase.ABName;
            item.m_DependAssetBundle = abBase.ABDependce;
            if (m_ResouceItemDic.ContainsKey(item.m_Crc))
            {
                Debug.LogError("重复的Crc 资源名:" + item.m_AssetName + " ab包名：" + item.m_ABName);
            }
            else
            {
                if (item.m_Crc == 4292853877)
                {
                    Debug.LogError("PDB加载上了");
                }
                m_ResouceItemDic.Add(item.m_Crc, item);
            }
        }
        return true;
    }

    /// <summary>
    /// 根据路径的crc加载中间类ResoucItem
    /// </summary>
    /// <param name="crc">全路径</param>
    /// <returns></returns>
    public ResouceItem LoadResouceAssetBundle(uint crc)
    {
        if (!m_ResouceItemDic.TryGetValue(crc, out ResouceItem item) || item == null)
        {
            Debug.LogError(string.Format("LoadResourceAssetBundle error: can not find crc {0} in AssetBundleConfig", crc.ToString()));
            return item;
        }

        item.m_AssetBundle = LoadAssetBundle(item.m_ABName);

        if (item.m_DependAssetBundle != null)
        {
            for (int i = 0; i < item.m_DependAssetBundle.Count; i++)
            {
                LoadAssetBundle(item.m_DependAssetBundle[i]);
            }
        }

        return item;
    }

    /// <summary>
    /// 加载单个assetbundle根据名字
    /// </summary>
    /// <param name="name">AB包名</param>
    /// <returns></returns>
    private AssetBundle LoadAssetBundle(string name)
    {
        uint crc = Crc32.GetCrc32(name);
        if (!m_AssetBundleItemDic.TryGetValue(crc, out AssetBundleItem item))
        {
          
            //修改全路径
            // string fullPath = ABLoadPath + name;

            string hotABPath = HotPatchManager.Instance.ComputeABPath(name);
            string fullPath = string.IsNullOrEmpty(hotABPath) ? ABLoadPath + name : hotABPath;

            //string str =Application.streamingAssetsPath+"/"+name;
            //byte[] bytes = AES.AESFileByteDecrypt(str, AesKey);
            byte[] bytes = AES.AESFileByteDecrypt(fullPath, AesKey);

            //AB加密需要替换下面代码
            //AssetBundle assetBundle = AssetBundle.LoadFromFile(fullPath);
            AssetBundle assetBundle = AssetBundle.LoadFromMemory(bytes);
            

            if (assetBundle == null)
               Debug.LogError(" Load AssetBundle Error:" + fullPath);
            
            item = m_AssetBundleItemPool.Spawn(true);
            item.assetBundle = assetBundle;
            item.RefCount++;
            m_AssetBundleItemDic.Add(crc, item);
        }
        else
        {
            item.RefCount++;
        }
        return item.assetBundle;
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    /// <param name="item"></param>
    public void ReleaseAsset(ResouceItem item)
    {
        if (item == null)
            return;

        if (item.m_DependAssetBundle != null && item.m_DependAssetBundle.Count > 0)
        {
            for (int i = 0; i < item.m_DependAssetBundle.Count; i++)
            {
                UnLoadAssetBundle(item.m_DependAssetBundle[i]);
            }
        }
        UnLoadAssetBundle(item.m_ABName);
    }

    private void UnLoadAssetBundle(string name)
    {
        uint crc = Crc32.GetCrc32(name);
        if (m_AssetBundleItemDic.TryGetValue(crc, out AssetBundleItem item) && item != null)
        {
            item.RefCount--;
            if (item.RefCount <= 0 && item.assetBundle != null)
            {
                item.assetBundle.Unload(true);
                item.Rest();
                m_AssetBundleItemPool.Recycle(item);
                m_AssetBundleItemDic.Remove(crc);
            }
        }
    }

    /// <summary>
    /// 根据crc查找ResouceItem
    /// </summary>
    /// <param name="crc"></param>
    /// <returns></returns>
    public ResouceItem FindResourceItme(uint crc)
    {
         m_ResouceItemDic.TryGetValue(crc, out ResouceItem item);
       // return m_ResouceItemDic[crc] == null ? null : m_ResouceItemDic[crc];
        return item;
    }
}
/// <summary>
/// AB包引用
/// </summary>
public class AssetBundleItem
{
    public AssetBundle assetBundle = null;
    public int RefCount;

    public void Rest()
    {
        assetBundle = null;
        RefCount = 0;
    }
}
/// <summary>
///单个对象的ab包类
/// </summary>
public class ResouceItem
{
    //资源路径的CRC
    public uint m_Crc = 0;
    //该资源的文件名
    public string m_AssetName = string.Empty;
    //该资源所在的AssetBundle
    public string m_ABName = string.Empty;
    //该资源所依赖的AssetBundle
    public List<string> m_DependAssetBundle = null;
    //该资源加载完的AB包
    public AssetBundle m_AssetBundle = null;

    //------------------------以下主要针对资源-----------------------------
    /// <summary>
    /// 加载出来的资源对象
    /// </summary>
    public Object m_Obj = null;
    /// <summary>
    /// 资源唯一标识
    /// </summary>
    public int m_Guid = 0;
    /// <summary>
    /// 资源最后所使用的时间
    /// </summary>
    public float m_LastUseTime = 0.0f;
    /// <summary>
    /// 资源引用计数
    /// </summary>
    protected int m_RefCount = 0;
    /// <summary>
    /// 是否跳场景清掉
    /// </summary>
    public bool m_Clear = true;
   
    public int RefCount
    {
        get { return m_RefCount; }
        set
        {
            m_RefCount = value;
            if (m_RefCount < 0)
            {
                Debug.LogError("refcount < 0" + m_RefCount + " ," + (m_Obj != null ? m_Obj.name : "name is null"));
            }
        }
    }
}
