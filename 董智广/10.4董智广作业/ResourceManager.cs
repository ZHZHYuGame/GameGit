using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.U2D;

/// <summary>
/// 加载资源的优先级
/// </summary>
public enum LoadResPriority
{
   
    /// <summary>
    /// 一般优先级
    /// </summary>
    RES_MIDDLE=0,
    /// <summary>
    /// 低优先级
    /// </summary>
    RES_SLOW,
    /// <summary>
    /// 最高优先级
    /// </summary>
    RES_HIGHT ,
    /// <summary>
    /// 优先级数量
    /// </summary>
    RES_NUM,
}

public class ResouceObj
{
    //路径对应CRC
    public uint m_Crc = 0;
    //存ResouceItem
    public ResouceItem m_ResItem = null;
    //实例化出来的GameObject
    public GameObject m_CloneObj = null;
    //是否跳场景清除
    public bool m_bClear = true;
    //储存GUID
    public long m_Guid = 0;
    //是否已经放回对象池  默认未放回对象池
    public bool m_Already = false;
    //--------------------------------
    //是否放到场景节点下面
    public bool m_SetSceneParent = false;
    //实例化资源加载完成回调:回调的是自己的逻辑方法
    public OnAsyncResFinish m_DealFinish = null;
    //异步参数
    public object m_Param1, m_Param2, m_Param3 = null;
    //离线数据
    public OfflineData m_OfflineData = null;

    public void Reset()
    {
        m_Crc = 0;
        m_CloneObj = null;
        m_bClear = true;
        m_Guid = 0;
        m_ResItem = null;
        m_Already = false;
        m_SetSceneParent = false;
        m_DealFinish = null;
        m_Param1 = m_Param2 = m_Param3 = null;
        m_OfflineData = null;
    }
}

/// <summary>
/// 异步加载Item类
/// </summary>
public class AsyncLoadResParam
{
    public List<AsyncCallBack> m_CallBackList = new List<AsyncCallBack>();
    public uint m_Crc;
    public string m_Path;
    /// <summary>
    /// 是否是图片
    /// </summary>
    public bool m_Sprite = false;
    public LoadResPriority m_Priority = LoadResPriority.RES_SLOW;

    public void Reset()
    {
        m_CallBackList.Clear();
        m_Crc = 0;
        m_Path = "";
        m_Sprite = false;
        m_Priority = LoadResPriority.RES_SLOW;
    }
}

/// <summary>
/// 回调数据
/// </summary>
public class AsyncCallBack
{
    //加载完成的回调(针对ObjectManager)
    public OnAsyncObjFinish   m_DealObjFinish = null;
    //ObjectManager对应的中间
    public ResouceObj m_ResObj = null;
    
    
    //---------------------------------------------
    //加载完成的回调
    public OnAsyncResFinish m_DealFinish = null;
    //回调参数
    public object m_Param1 = null, m_Param2 = null, m_Param3 = null;

    public void Reset()
    {
        m_DealFinish = null;
        m_DealObjFinish = null;
        m_Param1 = null;
        m_Param2 = null;
        m_Param3 = null;
        m_ResObj = null;
    }
}

/// <summary>
/// 资源加载回调 回调的是自己的逻辑方法
/// </summary>
/// <param name="path"></param>
/// <param name="obj"></param>
/// <param name="param1"></param>
/// <param name="param2"></param>
/// <param name="param3"></param>
public delegate void OnAsyncResFinish(string path, Object obj, object param1 = null, object param2 = null, object param3 = null);
//public delegate void OnAsyncObjFinish(string path, Object obj, params object[] param);

/// <summary>
/// 实例化对象加载完成回调
/// </summary>
/// <param name="path"></param>
/// <param name="resObj"></param>
/// <param name="param1"></param>
/// <param name="param2"></param>
/// <param name="param3"></param>
public delegate void OnAsyncObjFinish(string path, ResouceObj resObj, object param1 = null, object param2 = null, object param3 = null);

public class ResourceManager : Singleton<ResourceManager>
{
    protected long m_Guid = 0;
    public bool m_LoadFormAssetBundle = true;

    /// <summary>
    /// 缓存引用计数为零的资源列表，达到缓存最大的时候释放这个列表里面最早没用的资源
    /// </summary>
    protected CMapList<ResouceItem> m_NoRefrenceAssetMapList = new CMapList<ResouceItem>();

    /// <summary>
    /// 缓存使用的资源列表
    /// </summary>
    public Dictionary<uint, ResouceItem> AssetDic { get; set; } = new Dictionary<uint, ResouceItem>();

    /// <summary>
    /// 中间类，回调类的类对象池
    /// </summary>
    //protected ClassObjectPool<AsyncLoadResParam> m_AsyncLoadResParamPool = new ClassObjectPool<AsyncLoadResParam>(50);
    protected ClassObjectPool<AsyncLoadResParam> m_AsyncLoadResParamPool = ObjectManager.Instance.GetOrCreatClassPool<AsyncLoadResParam>(50);
    protected ClassObjectPool<AsyncCallBack> m_AsyncCallBackPool = ObjectManager.Instance.GetOrCreatClassPool<AsyncCallBack>(100);
    // protected ClassObjectPool<AsyncCallBack> m_AsyncCallBackPool = new ClassObjectPool<AsyncCallBack>(100);

    //Mono脚本
    protected MonoBehaviour m_Startmono;
    /// <summary>
    /// 正在异步加载的资源列表
    /// </summary>
    protected List<AsyncLoadResParam>[] m_LoadingAssetList = new List<AsyncLoadResParam>[(int)LoadResPriority.RES_NUM];
    /// <summary>
    /// 正在异步加载的Dic
    /// </summary>
    protected Dictionary<uint, AsyncLoadResParam> m_LoadingAssetDic = new Dictionary<uint, AsyncLoadResParam>();

    /// <summary>
    /// 最长连续卡着加载资源的时间，单位微妙
    /// </summary>
    private const long MAXLOADRESTIME = 200000;

    /// <summary>
    /// 最大缓存个数
    /// </summary>
    private const int MAXCACHECOUNT = 500;

    public void Init(MonoBehaviour mono)
    {
        for (int i = 0; i < (int)LoadResPriority.RES_NUM; i++)
        {
            m_LoadingAssetList[i] = new List<AsyncLoadResParam>();
        }
        m_Startmono = mono;
        m_Startmono.StartCoroutine(AsyncLoadCor());
    }

    /// <summary>
    /// 创建唯一的GUID
    /// </summary>
    /// <returns></returns>
    public long CreatGuid()
    {
        return m_Guid++;
    }

    /// <summary>
    /// 清空缓存
    /// </summary>
    public void ClearCache()
    {
        //List<ResouceItem> tempList = new List<ResouceItem>();
        foreach (ResouceItem item in AssetDic.Values)
        {
            if (item.m_Clear)
            {
                DestoryResouceItme(item, true);
                //tempList.Add(item);
            }
        }

        //foreach (ResouceItem item in tempList)
        //{
        //    DestoryResouceItme(item, true);
        //}
        //tempList.Clear();
    }

    /// <summary>
    /// 取消异步加载资源
    /// </summary>
    /// <returns></returns>
    public bool CancleLoad(ResouceObj res)
    {
        if (m_LoadingAssetDic.TryGetValue(res.m_Crc, out AsyncLoadResParam para) && m_LoadingAssetList[(int)para.m_Priority].Contains(para))
        {
            for (int i = para.m_CallBackList.Count; i >= 0; i--)
            {
                AsyncCallBack tempCallBack = para.m_CallBackList[i];
                if (tempCallBack != null && res == tempCallBack.m_ResObj)
                {
                    tempCallBack.Reset();
                    m_AsyncCallBackPool.Recycle(tempCallBack);
                    para.m_CallBackList.Remove(tempCallBack);
                }
            }

            if (para.m_CallBackList.Count <= 0)
            {
                para.Reset();
                m_LoadingAssetList[(int)para.m_Priority].Remove(para);
                m_AsyncLoadResParamPool.Recycle(para);
                m_LoadingAssetDic.Remove(res.m_Crc);
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 根据ResObj增加引用计数
    /// </summary>
    /// <returns></returns>
    public int IncreaseResouceRef(ResouceObj resObj, int count = 1)
    {
        return resObj != null ? IncreaseResouceRef(resObj.m_Crc, count) : 0;
    }

    /// <summary>
    /// 根据crc增加引用计数
    /// </summary>
    /// <param name="crc"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public int IncreaseResouceRef(uint crc = 0, int count = 1)
    {
        ResouceItem item = null;
        if (!AssetDic.TryGetValue(crc, out item) || item == null)
            return 0;

        item.RefCount += count;
        item.m_LastUseTime = Time.realtimeSinceStartup;

        return item.RefCount;
    }

    /// <summary>
    /// 根据ResouceObj减少引用计数
    /// </summary>
    /// <param name="resObj"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public int DecreaseResoucerRef(ResouceObj resObj, int count = 1)
    {
        return resObj != null ? DecreaseResoucerRef(resObj.m_Crc, count) : 0;
    }

    /// <summary>
    /// 根据crc用计数
    /// </summary>
    /// <param name="crc"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public int DecreaseResoucerRef(uint crc, int count = 1)
    {
        ResouceItem item = null;
        if (!AssetDic.TryGetValue(crc, out item) || item == null)
            return 0;

        item.RefCount -= count;

        return item.RefCount;
    }

    /// <summary>
    /// 预加载资源
    /// </summary>
    /// <param name="path">资源路径</param>
    /// <param name="isSprite">是否是图片</param>
    public void PreloadRes(string path,bool isSprite = false) 
    {
        if (string.IsNullOrEmpty(path))
            return;
        uint crc = Crc32.GetCrc32(path);
        ResouceItem item = GetCacheResouceItem(crc, 0);
        if (item != null)
            return;

        Object obj = null;
#if UNITY_EDITOR
        if (!m_LoadFormAssetBundle)
        {
            item = AssetBundleManager.Instance.FindResourceItme(crc);
            if (item != null && item.m_Obj != null)
            {
                obj = item.m_Obj ;
            }
            else
            {
                if (item == null)
                {
                    item = new ResouceItem();
                    item.m_Crc = crc;
                }
                if(isSprite)
                    obj = LoadAssetByEditor<Sprite>(path);
                else
                    obj = LoadAssetByEditor<Object>(path);
            }
        }
#endif

        if (obj == null)
        {
            item = AssetBundleManager.Instance.LoadResouceAssetBundle(crc);
            if (item != null && item.m_AssetBundle != null)
            {
                if (item.m_Obj != null)
                {
                    obj = item.m_Obj ;
                }
                else
                {
                    if(isSprite)
                        obj = item.m_AssetBundle.LoadAsset<Sprite>(item.m_AssetName);
                    else
                        obj = item.m_AssetBundle.LoadAsset<Object>(item.m_AssetName);
                }
            }
        }

        CacheResource(path, ref item, crc, obj);
        //跳场景不清空缓存
        item.m_Clear = false;
        ReleaseResouce(obj, false);
    }

    /// <summary>
    /// 同步加载资源，针对给ObjectManager的接口
    /// </summary>
    /// <param name="path"></param>
    /// <param name="resObj"></param>
    /// <returns></returns>
    public ResouceObj LoadResource(string path, ResouceObj resObj)
    {
        if (resObj == null)
        {
            return null;
        }

        uint crc = resObj.m_Crc == 0 ? Crc32.GetCrc32(path) : resObj.m_Crc;

        ResouceItem item = GetCacheResouceItem(crc);
        if (item != null)
        {
            resObj.m_ResItem = item;
            return resObj;
        }

        Object obj = null;
#if UNITY_EDITOR
        if (!m_LoadFormAssetBundle)
        {
            item = AssetBundleManager.Instance.FindResourceItme(crc);
            if (item != null && item.m_Obj != null)
            {
                obj = item.m_Obj as Object;
            }
            else
            {
                if (item == null)
                {
                    item = new ResouceItem();
                    item.m_Crc = crc;
                }
                obj = LoadAssetByEditor<Object>(path);
            }
        }
#endif

        if (obj == null)
        {
            item = AssetBundleManager.Instance.LoadResouceAssetBundle(crc);
            if (item != null && item.m_AssetBundle != null)
            {
                if (item.m_Obj != null)
                {
                    obj = item.m_Obj as Object;
                }
                else
                {
                    obj = item.m_AssetBundle.LoadAsset<Object>(item.m_AssetName);
                }
            }
        }

        CacheResource(path, ref item, crc, obj);
        resObj.m_ResItem = item;
        item.m_Clear = resObj.m_bClear;

        return resObj;
    }

    /// <summary>
    /// 同步资源加载，外部直接调用，仅加载不需要实例化的资源，例如Texture,音频等等
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    public T LoadResource<T>(string path) where T : UnityEngine.Object
    {
        // 检查传入的资源路径是否为空或null，如果是则返回null
        if (string.IsNullOrEmpty(path))
            return null;

        // 计算资源路径的CRC32校验值
        uint crc = Crc32.GetCrc32(path);

        // 尝试从缓存中获取资源项
        ResouceItem item = GetCacheResouceItem(crc);

        // 如果缓存中有该资源，则将其转换为泛型类型T并返回
        if (item != null)
            return item.m_Obj as T;

        // 定义泛型资源对象，初始为null
        T obj = null;

#if UNITY_EDITOR
        // 如果当前处于Unity编辑器环境，并且配置为不从AB包加载资源
        if (!m_LoadFormAssetBundle)
        {
            // 尝试从AssetBundleManager中查找资源项
            item = AssetBundleManager.Instance.FindResourceItme(crc);

            // 如果找到了资源项，并且资源项包含的AB包不为空
            if (item != null && item.m_AssetBundle != null)
            {
                // 如果资源对象已经加载，则直接转换类型并赋值给obj
                if (item.m_Obj != null)
                {
                    obj = (T)item.m_Obj;
                }
                else
                {
                    // 否则，从AB包中加载资源，并赋值给obj
                    obj = item.m_AssetBundle.LoadAsset<T>(item.m_AssetName);
                }
            }
            else
            {
                // 如果资源项为空，创建一个新的资源项并设置CRC值
                if (item == null)
                {
                    item = new ResouceItem();
                    item.m_Crc = crc;
                }
                // 从编辑器环境中直接加载资源
                obj = LoadAssetByEditor<T>(path);
            }
        }
#endif

        // 如果在非编辑器环境或从AB包中未找到资源，再次尝试从AssetBundleManager中加载资源
        if (obj == null)
        {
            item = AssetBundleManager.Instance.LoadResouceAssetBundle(crc);

            // 如果资源项和AB包都不为空，尝试从中加载资源
            if (item != null && item.m_AssetBundle != null)
            {
                if (item.m_Obj != null)
                {
                    // 如果资源对象已经加载，则直接转换类型并赋值给obj
                    obj = item.m_Obj as T;
                }
                else
                {
                    // 否则，从AB包中加载资源，并赋值给obj
                    obj = item.m_AssetBundle.LoadAsset<T>(item.m_AssetName);
                }
            }
        }

        // 将加载的资源缓存起来
        CacheResource(path, ref item, crc, obj);

        // 返回加载的资源对象
        return obj;
    }
    /// <summary>
    /// 根据ResouceObj卸载资源
    /// </summary>
    /// <param name="resObj"></param>
    /// <param name="destoryObj"></param>
    /// <returns></returns>
    public bool ReleaseResouce(ResouceObj resObj, bool destoryObj = false)
    {
        if (resObj == null)
            return false;

        ResouceItem item = null;
        if (!AssetDic.TryGetValue(resObj.m_Crc, out item) || null == item)
        {
            Debug.LogError("AssetDic里不存在改资源：" + resObj.m_CloneObj.name + "  可能释放了多次");
        }

        GameObject.Destroy(resObj.m_CloneObj);

        item.RefCount--;

        DestoryResouceItme(item, destoryObj);
        return true;
    }

    /// <summary>
    /// 不需要实例化的资源的卸载，根据对象
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="destoryObj"></param>
    /// <returns></returns>
    public bool ReleaseResouce(Object obj, bool destoryObj = false)
    {
        if (obj == null)
            return false;

        ResouceItem item = null;
        foreach (ResouceItem res in AssetDic.Values)
        {
            if (res.m_Guid == obj.GetInstanceID())
                item = res;
        }

        if (item == null)
        {
            Debug.LogError("AssetDic里不存在改资源：" + obj.name + "  可能释放了多次");
            return false;
        }

        item.RefCount--;

        DestoryResouceItme(item, destoryObj);
        return true;
    }

    /// <summary>
    /// 不需要实例化的资源卸载，根据路径
    /// </summary>
    /// <param name="path"></param>
    /// <param name="destoryObj"></param>
    /// <returns></returns>
    public bool ReleaseResouce(string path, bool destoryObj = false)
    {
        if (string.IsNullOrEmpty(path))
        {
            return false;
        }

        uint crc = Crc32.GetCrc32(path);
        if (!AssetDic.TryGetValue(crc, out ResouceItem item) || null == item)
        {
            Debug.LogError("AssetDic里不存在改资源：" + path + "  可能释放了多次");
        }

        item.RefCount--;

        DestoryResouceItme(item, destoryObj);
        return true;
    }

    /// <summary>
    /// 缓存加载的资源
    /// </summary>
    /// <param name="path"></param>
    /// <param name="item"></param>
    /// <param name="crc"></param>
    /// <param name="obj"></param>
    /// <param name="addrefcount"></param>
    void CacheResource(string path, ref ResouceItem item, uint crc, Object obj, int addrefcount = 1)
    {
        //缓存太多，清除最早没有使用的资源
        WashOut();

        if (item == null)
            Debug.LogError("ResouceItem is null, path: " + path);

        if (obj == null)
            Debug.LogError("ResouceLoad Fail :  " + path);

        item.m_Obj = obj;
        item.m_Guid = obj.GetInstanceID();
        item.m_LastUseTime = Time.realtimeSinceStartup;
        item.RefCount += addrefcount;
        if (AssetDic.TryGetValue(item.m_Crc, out ResouceItem oldItme))
            AssetDic[item.m_Crc] = item;
        else
            AssetDic.Add(item.m_Crc, item);
    }

    /// <summary>
    /// 缓存太多，清除最早没有使用的资源
    /// </summary>
    protected void WashOut()
    {
        //当大于缓存个数时，进行一半释放
        while (m_NoRefrenceAssetMapList.Size() >= MAXCACHECOUNT)
        {
            for (int i = 0; i < MAXCACHECOUNT / 2; i++)
            {
                ResouceItem item = m_NoRefrenceAssetMapList.Back();
                DestoryResouceItme(item, true);
            }
        }
    }

    /// <summary>
    /// 回收一个资源
    /// </summary>
    /// <param name="item"></param>
    /// <param name="destroy"></param>
    protected void DestoryResouceItme(ResouceItem item, bool destroyCache = false)
    {
        if (item == null || item.RefCount > 0)
            return;

        if (!destroyCache)
        {
            m_NoRefrenceAssetMapList.InsertToHead(item);
            return;
        }
        if (!AssetDic.Remove(item.m_Crc))
            return;

        m_NoRefrenceAssetMapList.Remove(item);
        //释放assetbundle引用
        AssetBundleManager.Instance.ReleaseAsset(item);
        //清空资源对应的对象池
        ObjectManager.Instance.ClearPoolObject(item.m_Crc);
        if (item.m_Obj != null)
        {
            item.m_Obj = null;
#if UNITY_EDITOR
            Resources.UnloadUnusedAssets();
#endif
        }
    }

#if UNITY_EDITOR
    /// <summary>
    /// 从编辑器加载资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    protected T LoadAssetByEditor<T>(string path) where T : UnityEngine.Object
    {
        return UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);
    }
#endif

    /// <summary>
    /// 从资源池获取缓存资源
    /// </summary>
    /// <param name="crc"></param>
    /// <param name="addrefcount">增加的数量</param>
    /// <returns></returns>
    ResouceItem GetCacheResouceItem(uint crc, int addrefcount = 1)
    {
        if (AssetDic.TryGetValue(crc, out ResouceItem item))
        {
            if (item != null)
            {
                item.RefCount += addrefcount;
                item.m_LastUseTime = Time.realtimeSinceStartup;//记录了自游戏开始运行以来经过的时间，单位是秒
            }
        }

        return item;
    }

    /// <summary>
    /// 异步加载资源（仅仅是不需要实例化的资源，例如音频，图片等等）
    /// </summary>
    /// <param name="path">资源路径</param>
    /// <param name="dealFinish">加载完成回调</param>
    /// <param name="priority">该加载资源的优先级</param>
    /// <param name="isSprite">是否是图片</param>
    /// <param name="param1"></param>
    /// <param name="param2"></param>
    /// <param name="param3"></param>
    /// <param name="crc">该资源的Crc</param>
    public void AsyncLoadResource(string path, OnAsyncResFinish dealFinish, LoadResPriority priority, bool isSprite = false, 
        object param1 = null, object param2 = null, object param3 = null, uint crc = 0)
    {
        if (crc == 0)
            crc = Crc32.GetCrc32(path);

        ResouceItem item = GetCacheResouceItem(crc);
        if (item != null)
        {
            dealFinish?.Invoke(path, item.m_Obj, param1, param2, param3);
            return;
        }

        //判断是否在加载中
        if (!m_LoadingAssetDic.TryGetValue(crc, out AsyncLoadResParam para) || para == null)
        {
            para = m_AsyncLoadResParamPool.Spawn(true);
            para.m_Crc = crc;
            para.m_Path = path;
            para.m_Sprite = isSprite;
            para.m_Priority = priority;
            m_LoadingAssetDic.Add(crc, para);
            m_LoadingAssetList[(int)priority].Add(para);
        }

        AsyncCallBack callBack = m_AsyncCallBackPool.Spawn(true);
        callBack.m_DealFinish = dealFinish;
        callBack.m_Param1 = param1;
        callBack.m_Param2 = param2;
        callBack.m_Param3 = param3;
        para.m_CallBackList.Add(callBack);
    }

    /// <summary>
    /// 新图集加载方法
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public Sprite LoadSpriteBySpriteAtlas(string path)
    {
        string spriteName = Path.GetFileNameWithoutExtension(path);
        //string spriteName = path.Remove(0, path.LastIndexOf('/') + 1);
        //spriteName = spriteName.Remove(spriteName.LastIndexOf('.'));
        string filePath = path.Remove(path.LastIndexOf('/'));
        string spriteAtlasName = filePath.Remove(0, filePath.LastIndexOf('/') + 1);
        string spriteAtlasPath = string.Format("{0}/{1}.spriteatlas", filePath, spriteAtlasName);
        SpriteAtlas spriteAtlas = LoadResource<SpriteAtlas>(spriteAtlasPath);
        if (spriteAtlas == null)
        {
            Debug.LogError("不存在该图集：" + spriteAtlasPath);
            return null;
        }
        else
        {
            return spriteAtlas.GetSprite(spriteName);
        }

    }

    

    /// <summary>
    /// 针对ObjectManager的异步加载接口
    /// </summary>
    /// <param name="path"></param>
    /// <param name="resObj"></param>
    /// <param name="dealfinish"></param>
    /// <param name="priority"></param>
    public void AsyncLoadResource(string path, ResouceObj resObj, OnAsyncObjFinish dealfinish, LoadResPriority priority)
    {
        ResouceItem item = GetCacheResouceItem(resObj.m_Crc);
        if (item != null)
        {
            resObj.m_ResItem = item;
            dealfinish?.Invoke(path, resObj);
            return;
        }

        //判断是否在加载中
        if (!m_LoadingAssetDic.TryGetValue(resObj.m_Crc, out AsyncLoadResParam para) || para == null)
        {
            para = m_AsyncLoadResParamPool.Spawn(true);
            para.m_Crc = resObj.m_Crc;
            para.m_Path = path;
            para.m_Priority = priority;
            m_LoadingAssetDic.Add(resObj.m_Crc, para);
            m_LoadingAssetList[(int)priority].Add(para);
        }

        //往回调列表里面加回调
        AsyncCallBack callBack = m_AsyncCallBackPool.Spawn(true);
        callBack.m_DealObjFinish = dealfinish;
        callBack.m_ResObj = resObj;
        para.m_CallBackList.Add(callBack);
    }

    /// <summary>
    /// 异步加载
    /// </summary>
    /// <returns></returns>
    IEnumerator AsyncLoadCor()
    {
        List<AsyncCallBack> callBackList = null;
        //上一次yield的时间
        long lastYiledTime = System.DateTime.Now.Ticks;
       
        while (true)
        {
            bool haveYield = false;
            for (int i = 0; i < (int)LoadResPriority.RES_NUM; i++)
            {
                if (m_LoadingAssetList[(int)LoadResPriority.RES_HIGHT].Count > 0)
                    i = (int)LoadResPriority.RES_HIGHT;
                else if (m_LoadingAssetList[(int)LoadResPriority.RES_MIDDLE].Count > 0)
                    i = (int)LoadResPriority.RES_MIDDLE;

                List<AsyncLoadResParam> loadingList = m_LoadingAssetList[i];
                if (loadingList.Count <= 0)
                    continue;

                AsyncLoadResParam loadingItem = loadingList[0];
                loadingList.RemoveAt(0);
                callBackList = loadingItem.m_CallBackList;

                Object obj = null;
                ResouceItem item = null;

#if UNITY_EDITOR
                if (!m_LoadFormAssetBundle)
                {

                    if (loadingItem.m_Sprite)
                        obj = LoadAssetByEditor<Sprite>(loadingItem.m_Path);
                    else
                        obj = LoadAssetByEditor<Object>(loadingItem.m_Path);
                    //模拟异步加载
                    yield return new WaitForSeconds(1f);

                    item = AssetBundleManager.Instance.FindResourceItme(loadingItem.m_Crc);
                    if (item == null)
                    {
                        item = new ResouceItem
                        {
                            m_Crc = loadingItem.m_Crc
                        };
                    }
                }
#endif
                if (obj == null)
                {
                    item = GetCacheResouceItem(loadingItem.m_Crc);
                    if (item == null)
                        item = AssetBundleManager.Instance.LoadResouceAssetBundle(loadingItem.m_Crc);
                    if (item != null && item.m_AssetBundle != null)
                    {
                        AssetBundleRequest abRequest = null;

                        if (loadingItem.m_Sprite)
                            abRequest = item.m_AssetBundle.LoadAssetAsync<Sprite>(item.m_AssetName);
                        else
                            abRequest = item.m_AssetBundle.LoadAssetAsync(item.m_AssetName);
                        yield return abRequest;//等待异步加载资源完成
                        if (abRequest.isDone)
                            obj = abRequest.asset;
                        lastYiledTime = System.DateTime.Now.Ticks;
                    }
                }

                CacheResource(loadingItem.m_Path, ref item, loadingItem.m_Crc, obj, callBackList.Count);

                for (int j = 0; j < callBackList.Count; j++)
                {
                    AsyncCallBack callBack = callBackList[j];

                    if (callBack != null && callBack.m_DealObjFinish != null && callBack.m_ResObj != null)
                    {
                        ResouceObj tempResObj = callBack.m_ResObj;
                        tempResObj.m_ResItem = item;
                        callBack.m_DealObjFinish(loadingItem.m_Path, tempResObj, tempResObj.m_Param1, tempResObj.m_Param2, tempResObj.m_Param3);
                        callBack.m_DealObjFinish = null;
                        tempResObj = null;
                    }

                    if (callBack != null && callBack.m_DealFinish != null)
                    {
                       // callBack.m_DealObjFinish?.Invoke(loadingItem.m_Path, obj, callBack.m_Param1, callBack.m_Param2, callBack.m_Param3)
                        callBack.m_DealFinish(loadingItem.m_Path, obj, callBack.m_Param1, callBack.m_Param2, callBack.m_Param3);
                        callBack.m_DealFinish = null;
                    }

                    callBack.Reset();
                    m_AsyncCallBackPool.Recycle(callBack);
                }

                obj = null;
                callBackList.Clear();
                m_LoadingAssetDic.Remove(loadingItem.m_Crc);

                loadingItem.Reset();
                m_AsyncLoadResParamPool.Recycle(loadingItem);

                if (System.DateTime.Now.Ticks - lastYiledTime > MAXLOADRESTIME)
                {
                    yield return null;
                    lastYiledTime = System.DateTime.Now.Ticks;
                    haveYield = true;
                }
            }

            if (!haveYield || System.DateTime.Now.Ticks - lastYiledTime > MAXLOADRESTIME)
            {
                lastYiledTime = System.DateTime.Now.Ticks;
                yield return null;
            }

        }
    }
}

/// <summary>
/// 双向链表结构节点
/// </summary>
/// <typeparam name="T"></typeparam>
public class Node<T> where T : class, new()
{
    /// <summary>
    /// 前一个节点
    /// </summary>
    public Node<T> left = null;
    /// <summary>
    /// 后一个节点
    /// </summary>
    public Node<T> right = null;
    /// <summary>
    /// 当前节点
    /// </summary>
    public T t = null;
}

/// <summary>
/// 双向链表结构
/// </summary>
/// <typeparam name="T"></typeparam>
public class DoubleLinedList<T> where T : class, new()
{
    /// <summary>
    /// 表头
    /// </summary>
    public Node<T> Head = null;
    /// <summary>
    /// 表尾
    /// </summary>
    public Node<T> Tail = null;
    /// <summary>
    /// 双向链表结构类对象池
    /// </summary>
    protected ClassObjectPool<Node<T>> doubleLinkNodePool = ObjectManager.Instance.GetOrCreatClassPool<Node<T>>(500);
    /// <summary>
    /// 目前个数
    /// </summary>
    protected int m_Count = 0;
    public int Count
    {
        get { return m_Count; }
    }

    /// <summary>
    /// 添加一个节点到头部
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public Node<T> AddToHeader(T t)
    {
        Node<T> pList = doubleLinkNodePool.Spawn(true);
        pList.right = null;
        pList.left = null;
        pList.t = t;
        return AddToHeader(pList);
    }




    /// <summary>
    /// 添加一个节点到头部
    /// </summary>
    /// <param name="pNode"></param>
    /// <returns></returns>
    public Node<T> AddToHeader(Node<T> pNode)
    {
        if (pNode == null)
            return null;

        pNode.left = null;
        if (Head == null)
        {
            Head = Tail = pNode;
        }
        else
        {
            pNode.right = Head;
            Head.left = pNode;
            Head = pNode;
        }
        m_Count++;
        return Head;
    }

    /// <summary>
    /// 添加节点到尾部
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public Node<T> AddToTail(T t)
    {
        Node<T> pList = doubleLinkNodePool.Spawn(true);
        pList.right = null;
        pList.left = null;
        pList.t = t;
        return AddToTail(pList);
    }

    /// <summary>
    /// 添加节点到尾部
    /// </summary>
    /// <param name="pNode"></param>
    /// <returns></returns>
    public Node<T> AddToTail(Node<T> pNode)
    {
        if (pNode == null)
            return null;

        pNode.right = null;
        if (Tail == null)
        {
            Head = Tail = pNode;
        }
        else
        {
            pNode.left = Tail;
            Tail.right = pNode;
            Tail = pNode;
        }
        m_Count++;
        return Tail;
    }

    /// <summary>
    /// 移除某个节点
    /// </summary>
    /// <param name="pNode"></param>
    public void RemoveNode(Node<T> pNode)
    {
        if (pNode == null)
            return;

        if (pNode == Head)
            Head = pNode.right;

        if (pNode == Tail)
            Tail = pNode.left;

        if (pNode.left != null)
            pNode.left.right = pNode.right;

        if (pNode.right != null)
            pNode.right.left = pNode.left;

        pNode.right = pNode.left = null;
        pNode.t = null;
        doubleLinkNodePool.Recycle(pNode);
        m_Count--;
    }

    /// <summary>
    /// 把某个节点移动到头部
    /// </summary>
    /// <param name="pNode"></param>
    public void MoveToHead(Node<T> pNode)
    {
        if (pNode == null || pNode == Head)
            return;

        if (pNode.left == null && pNode.right == null)
            return;

        if (pNode == Tail)
            Tail = pNode.left;

        if (pNode.left != null)
            pNode.left.right = pNode.right;

        if (pNode.right != null)
            pNode.right.left = pNode.left;

        pNode.left = null;
        pNode.right = Head;
        Head.left = pNode;
        Head = pNode;
        if (Tail == null)
        {
            Tail = Head;
        }
    }
}

public class CMapList<T> where T : class, new()
{
    DoubleLinedList<T> m_DLink = new DoubleLinedList<T>();//双向链表
    Dictionary<T, Node<T>> m_FindMap = new Dictionary<T, Node<T>>();
    
    ~CMapList()
    {
        Clear();
    }


    /// <summary>
    /// 清空列表
    /// </summary>
    public void Clear()
    {
        while (m_DLink.Tail != null)
        {
            Remove(m_DLink.Tail.t);
        }
    }

    /// <summary>
    /// 插入一个节点到表头
    /// </summary>
    /// <param name="t"></param>
    public void InsertToHead(T t)
    {
        if (m_FindMap.TryGetValue(t, out Node<T> node) && node != null)
        {
            m_DLink.AddToHeader(node);
            return;
        }
        m_DLink.AddToHeader(t);
        m_FindMap.Add(t, m_DLink.Head);
    }

    /// <summary>
    /// 从表尾弹出一个结点
    /// </summary>
    public void Pop()
    {
        if (m_DLink.Tail != null)
            Remove(m_DLink.Tail.t);
    }

    /// <summary>
    /// 删除某个节点
    /// </summary>
    /// <param name="t"></param>
    public void Remove(T t)
    {
        if (!m_FindMap.TryGetValue(t, out Node<T> node) || node == null)
            return;
        m_DLink.RemoveNode(node);
        m_FindMap.Remove(t);
    }

    /// <summary>
    /// 获取到尾部节点
    /// </summary>
    /// <returns></returns>
    public T Back()
    {
        return m_DLink.Tail == null ? null : m_DLink.Tail.t;
    }

    /// <summary>
    /// 返回节点个数
    /// </summary>
    /// <returns></returns>
    public int Size()
    {
        return m_FindMap.Count;
    }

    /// <summary>
    /// 查找是否存在该节点
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public Node<T> Find(T t)
    {
        if (!m_FindMap.TryGetValue(t, out Node<T> node) || node == null)
            return null;

        return node;
    }

    /// <summary>
    /// 刷新某个节点，把节点移动到头部
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public bool Reflesh(T t)
    {
        if (!m_FindMap.TryGetValue(t, out Node<T> node) || node == null)
            return false;

        m_DLink.MoveToHead(node);
        return true;
    }
}
