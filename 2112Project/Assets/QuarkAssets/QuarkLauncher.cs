using UnityEngine;
using System.IO;
using Quark.Asset;
using System;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Quark
{
    /// <summary>
    /// Quark配置脚本，挂载到物体上配置即可；
    /// </summary>
    public class QuarkLauncher : MonoBehaviour
    {
        [SerializeField] bool autoStartBasedOnConfig = true;
        /// <summary>
        /// 资源存储地址；
        /// </summary>
        [SerializeField] QuarkBuildPath quarkBuildPath;
        /// <summary>
        ///启用 ab build 的相对路径；
        /// </summary>
        [SerializeField] bool enableStreamingRelativeBuildPath;
        /// <summary>
        /// ab Build 的相对地址；
        /// </summary>
        [SerializeField] string streamingRelativeBuildPath;

        /// <summary>
        ///启用 ab build 的相对路径；
        /// </summary>
        [SerializeField] bool enablePersistentRelativeBundlePath;
        /// <summary>
        /// ab Build 的相对地址；
        /// </summary>
        [SerializeField] string persistentRelativeBundlePath;

        /// <summary>
        /// 加载模式，分别为Editor与Build；
        /// </summary>
        [SerializeField] QuarkLoadMode loadMode;
        /// <summary>
        /// QuarkAssetLoadMode 下AssetDatabase模式所需的寻址数据；
        /// <see cref="Quark.QuarkLoadMode"/>
        /// </summary>
        [SerializeField] QuarkDataset quarkDataset;
        /// <summary>
        /// 对称加密密钥；
        /// </summary>
        [SerializeField] string manifestAesKey;
        /// <summary>
        /// 加密偏移量；
        /// </summary>
        [SerializeField] ulong encryptionOffset;
        static QuarkLauncher instance;
        public static QuarkLauncher Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = GameObject.FindObjectOfType<QuarkLauncher>();
                    if (instance == null)
                    {
                        var go = new GameObject(typeof(QuarkLauncher).Name);
                        instance = go.AddComponent<QuarkLauncher>();
                    }
                }
                return instance;
            }
        }
        /// <summary>
        /// 根据需要启动当前配置内容；
        /// </summary>
        public void LaunchWithConfig(Action onSuccess, Action<string> onFailure)
        {
            switch (loadMode)
            {
                case QuarkLoadMode.AssetDatabase:
                    {
                        QuarkResources.LaunchAssetDatabaseMode(quarkDataset, onSuccess, onFailure);
                    }
                    break;
                case QuarkLoadMode.AssetBundle:
                    {
                        var dirPath = string.Empty;
                        switch (quarkBuildPath)
                        {
                            case QuarkBuildPath.StreamingAssets:
                                {
                                    #region streamingAssetPath
                                    if (enableStreamingRelativeBuildPath)
                                        dirPath = Path.Combine(Application.streamingAssetsPath, streamingRelativeBuildPath);
                                    else
                                        dirPath = Application.streamingAssetsPath;
                                    #endregion;
                                }
                                break;
                            case QuarkBuildPath.PersistentDataPath:
                                {
                                    #region persistentPath
                                    if (enablePersistentRelativeBundlePath)
                                        dirPath = Path.Combine(Application.persistentDataPath, persistentRelativeBundlePath);
                                    else
                                        dirPath = Application.persistentDataPath;
                                    #endregion;
                                }
                                break;
                        }
                        QuarkResources.LaunchAssetBundleMode(dirPath, onSuccess, onFailure, manifestAesKey, encryptionOffset);
                    }
                    break;
            }
        }
        void Awake()
        {
            instance = this;
            if (autoStartBasedOnConfig)
            {
                LaunchWithConfig(OnLaunchSuccess, OnLaunchFailure);
            }
        }
        void OnLaunchSuccess()
        {
            QuarkUtility.LogInfo($"{loadMode} launch success");
        }
        void OnLaunchFailure(string errorMsg)
        {
            QuarkUtility.LogInfo($"{loadMode} launch fail: {errorMsg}");
        }


        private void OnGUI()
        {
            if (GUI.Button(new Rect(new Vector2(200, 100), new Vector2(100, 100)), "点击加载测试的游戏对象资源"))
            {
                var cube = QuarkResources.LoadAsset<GameObject>("Cube");
                var cylinder = QuarkResources.LoadAsset<GameObject>("Cylinder");
                //在主线程上进行的一个异步操作;
               // var sphere = await QuarkResources.LoadAssetAsync<GameObject>("Sphere");

                Instantiate(cube, Vector3.zero, Quaternion.identity);
                Instantiate(cylinder, Vector3.up, Quaternion.identity);
            }

           
            if (GUI.Button(new Rect(new Vector2(200, 200), new Vector2(100, 100)), "点击加载测试的场景"))
            {
                //这是利用协程进行的场景的异步加载;additive参数为true的时候作为附加的场景添加;为false的时候作为单一场景出现;
                QuarkResources.LoadSceneAsync("Test", (time) =>
                {
                    //这个是加载进度是一个百分比的值;出现异步操作的过程每一帧会触发一次;
                    print("Progress: " + time);
                }, () =>
                {
                    //这是加载完成的回调;
                    print("GO Scene success");
                },true);
            }

            if (GUI.Button(new Rect(new Vector2(200, 300), new Vector2(100, 100)), "可以打断点,点击测试QuarkResourceAPI"))
            {
                //获取资源的信息;
                var isGetObjectInfo = QuarkResources.GetObjectInfo("Cube", out var info1);
                if (isGetObjectInfo)
                {
                    print("资源信息:" + info1.AssetPath);
                }

                //获取AssetBundle的信息;
                var isGetBundleInfo = QuarkResources.GetBundleInfo("assets_myprefabs_myscenes", out var info);
                if (isGetBundleInfo)
                {
                    print("Bundle的信息:" + info.ObjectCount);
                }

                //获取一个AssetBundle里面的所有的资源;
                var allObjects = QuarkResources.LoadAllAssets("assets_myprefabs_myscenes");

                //同步加载预制体;
                var obj = QuarkResources.LoadPrefab("Cube", true);

                //用来切换加载器,使用指定的加载模式加载;用AssetBundle还是AssetDatabase;
                //QuarkResources.ResetLoader(QuarkLoadMode.AssetBundle);

                //异步加载预制体;
                QuarkResources.LoadPrefabAsync("Cylinder", (x) =>
                {
                    print("cylinder 加载完成");
                }, true);

                //下载资源清单文件;
                //var nerResponse=  QuarkResources.DownloadRemoteManifest("Http://1.0.0.1");

                //加载一个资源和身上所有的依赖的组件;例如加载一个Cube,返回的集合里面会有依赖的Transform组件和MeshRenderer和Meshfilter等;
                var objs = QuarkResources.LoadMainAndSubAssets<Object>("Cube");

                //卸载所有的AssetBundle;
                QuarkResources.UnloadAllAssetBundle();
            }
        }
    }
}
