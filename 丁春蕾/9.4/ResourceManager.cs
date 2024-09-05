using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// 大文件管理;暂时没用上;
/// </summary>
public class BigObjectManager : MonoBehaviour
{
    //单例
    private static BigObjectManager _instance;
    public static BigObjectManager Instance => _instance ??= new GameObject("BigObjectManager").AddComponent<BigObjectManager>();
    private readonly ConcurrentQueue<Object> _bigFileQueue = new();

    public event Action<Object> OnGettingBigObject;


    void Update()
    {
        if (_bigFileQueue.Count > 0)
        {
            if (_bigFileQueue.TryDequeue(out var obj))
            {
                OnGettingBigObject?.Invoke(obj);
            }
        }
    }


}

public class ResourceManager : MonoBehaviour
{
    private static ResourceManager _instance;
    public static ResourceManager Instance => _instance ??= new GameObject("ResourceManager").AddComponent<ResourceManager>();

    //Ab包加载的类;
    private readonly AssetBundleManager _assetBundleManager = new();

    //资源缓存的类;
    private readonly AssetCache _assetCache = new();
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// 同步加载资源;
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="bundleName"></param>
    /// <param name="assetName"></param>
    /// <returns></returns>
    public T LoadAsset<T>(string bundleName, string assetName) where T : Object
    {
        RefCountedResource resource = _assetCache.GetFromCache(assetName);
        if (resource != null)
        {
            resource.AddRef();
            return resource.Resource as T;
        }

        var bundle = _assetBundleManager.LoadAssetBundle(bundleName);

        if (bundle == null)
        {
            Debug.LogError($"Failed to load AssetBundle: {bundleName}");
            return null;
        }


        var asset = bundle.Bundle.LoadAsset<T>(assetName);

        if (asset == null)
        {
            Debug.LogError($"Failed to load asset: {assetName}");
            return null;
        }

        var refCountedResource = new RefCountedResource(asset);
        _assetCache.AddToCache(assetName, refCountedResource);
        return asset;
    }

    /// <summary>
    /// 卸载资源;
    /// </summary>
    /// <param name="assetName"></param>
    public void UnloadAsset(string assetName)
    {
        RefCountedResource resource = _assetCache.GetFromCache(assetName);
        if (resource != null)
        {
            int refCount = resource.Release();
            // if (refCount == 0)
            // {
            //     //如果引用为0了,就从缓存中移除;
            //     _assetCache.RemoveFromCache(assetName);
            // }
            //这里使用LRU缓存策略;
        }
    }

    /// <summary>
    /// 主动卸载所有的未引用的资源,一般游戏帧数影响不大的时候调用;
    /// </summary>
    public void UnloadAllUnused()
    {
        //查找缓存里面的所有的引用数量为0的资源
        //清除缓存的数据;
        _assetCache.ClearUnusedResources();
        //Resources.UnloadUnusedAssets();
        GC.Collect();
    }

    /// <summary>
    /// 异步卸载资源;
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="bundleName"></param>
    /// <param name="assetName"></param>
    /// <returns></returns>
    public T LoadAssetAsync<T>(string bundleName, string assetName) where T : Object
    {
        RefCountedResource resource = _assetCache.GetFromCache(assetName);
        if (resource != null)
        {
            resource.AddRef();
            return resource.Resource as T;
        }

        var bundle = _assetBundleManager.LoadAssetBundle(bundleName);

        if (bundle == null)
        {
            Debug.LogError($"Failed to load AssetBundle: {bundleName}");
            return null;
        }
        var asset = bundle.Bundle.LoadAssetAsync<T>(assetName);
        asset.completed += operation =>
        {
            var refCountedResource = new RefCountedResource(asset.asset);
            _assetCache.AddToCache(assetName, refCountedResource);
        };
        return asset.asset as T;
    }

    /// <summary>
    /// 异步卸载资源;
    /// </summary>
    /// <param name="assetName"></param>
    /// 
    public void UnloadAssetAsync(string assetName)
    {
    }

      public void UnloadAssetBundle(string bundleName)
    {
        _assetBundleManager.UnloadAssetBundle(bundleName);
    }

}


/// <summary>
/// 资源引用计数;
/// </summary>
public class RefCountedResource
{
    public Object Resource { get; }
    private int _refCount;

    public RefCountedResource(Object resource)
    {
        Resource = resource;
        _refCount = 1;
    }

    public void AddRef() => _refCount++;

    public int Release() => --_refCount;
}

/// <summary>
/// 缓存策略;
/// </summary>
internal class AssetCache
{
    private readonly LRUCache<string, RefCountedResource> _cache = new LRUCache<string, RefCountedResource>(100);

    public void AddToCache(string key, RefCountedResource resource)
    {
        _cache.Put(key, resource);
    }

    public RefCountedResource GetFromCache(string key)
    {
        return _cache.Get(key);
    }

    public void RemoveFromCache(string key)
    {
        _cache.Remove(key);
    }

    //清除缓存里面引用数量为0的资源
    public void ClearUnusedResources()
    {
        var keysToRemove = _cache.CacheMap.Values.Where(resource => resource.Value.value.Release() == 0).Select(resource => resource.Value.key).ToList();
        foreach (var key in keysToRemove)
        {
            _cache.Remove(key);
        }
    }

}

public class LRUCache<K, V>
{
    private readonly Dictionary<K, LinkedListNode<(K key, V value)>> _cacheMap = new();
    private readonly LinkedList<(K key, V value)> _lruList = new();
    private readonly int _capacity;


    public LRUCache(int capacity)
    {
        _capacity = capacity;
    }

    //_cacheMap变量的属性
    public Dictionary<K, LinkedListNode<(K key, V value)>> CacheMap => _cacheMap;

    public V Get(K key)
    {
        if (CacheMap.TryGetValue(key, out var node))
        {
            _lruList.Remove(node);
            _lruList.AddFirst(node);
            return node.Value.value;
        }
        return default;
    }

    public void Put(K key, V value)
    {
        if (CacheMap.TryGetValue(key, out var node))
        {
            _lruList.Remove(node);
        }
        else if (CacheMap.Count >= _capacity)
        {
            var lru = _lruList.Last.Value;
            CacheMap.Remove(lru.key);
            _lruList.RemoveLast();
        }

        var newNode = new LinkedListNode<(K, V)>((key, value));
        _lruList.AddFirst(newNode);
        CacheMap[key] = newNode;
    }

    public void Remove(K key)
    {
        if (CacheMap.TryGetValue(key, out var node))
        {
            _lruList.Remove(node);
            CacheMap.Remove(key);
        }
    }
}


/// <summary>
/// 引用计数的 AssetBundle 管理类
/// </summary>
public class AssetBundleManager
{
    private readonly Dictionary<string, RefCountedAssetBundle> _loadedBundles = new();

    /// <summary>
    /// 加载 AssetBundle
    /// </summary>
    /// <param name="bundleName"></param>
    /// <returns></returns>
    public RefCountedAssetBundle LoadAssetBundle(string bundleName)
    {
        if (_loadedBundles.TryGetValue(bundleName, out var refCountedBundle))
        {
            refCountedBundle.AddRef();
            return refCountedBundle; // 如果已经加载，增加引用计数并返回
        }

        var path = $"Assets/AssetBundles/{bundleName}";
        var bundle = AssetBundle.LoadFromFile(path);

        if (bundle == null)
        {
            Debug.LogError($"Failed to load AssetBundle: {bundleName}");
            return null;
        }

        refCountedBundle = new RefCountedAssetBundle(bundle);
        _loadedBundles[bundleName] = refCountedBundle;
        Debug.Log($"AssetBundle loaded: {bundleName}");
        return refCountedBundle;
    }

    /// <summary>
    /// 卸载 AssetBundle
    /// </summary>
    /// <param name="bundleName"></param>
    public void UnloadAssetBundle(string bundleName, bool unloadAllLoadedObjects = false)
    {
        if (_loadedBundles.TryGetValue(bundleName, out var refCountedBundle))
        {
            int refCount = refCountedBundle.Release();
            if (refCount == 0)
            {
                refCountedBundle.Bundle.Unload(unloadAllLoadedObjects);
                _loadedBundles.Remove(bundleName);
                Debug.Log($"AssetBundle unloaded: {bundleName}");
            }
        }
    }

    /// <summary>
    /// 卸载所有 AssetBundle
    /// </summary>
    public void UnloadAll()
    {
        foreach (var bundle in _loadedBundles.Values)
        {
            bundle.Bundle.Unload(true);
        }
        _loadedBundles.Clear();
        Debug.Log("All AssetBundles unloaded.");
    }
}

/// <summary>
/// 引用计数的 AssetBundle 类
/// </summary>
public class RefCountedAssetBundle
{
    public AssetBundle Bundle { get; }
    private int _refCount;

    public RefCountedAssetBundle(AssetBundle bundle)
    {
        Bundle = bundle;
        _refCount = 1; // 初始化引用计数为1
    }

    public void AddRef() => _refCount++;

    public int Release() => --_refCount; // 返回减少后的引用计数
}