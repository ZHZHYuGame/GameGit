using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AsyncPrefabLoader : MonoBehaviour
{
    public string modelPath = "Models/MyModel";

    private void Start()
    { 
        StartCoroutine(LoadModelAsync());
    }

    private IEnumerator LoadModelAsync()
    {
        // 异步加载模型
        var request = UnityWebRequestAssetBundle.GetAssetBundle("file://" + modelPath);

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError("Error loading model: " + request.error);
        }
        else
        {
            // 从AssetBundle加载模型
            var bundle = DownloadHandlerAssetBundle.GetContent(request);
            GameObject model = bundle.LoadAsset<GameObject>("MyModel");
            Instantiate(model, Vector3.zero, Quaternion.identity);

            // 不要忘记卸载AssetBundle以释放内存
            bundle.Unload(true);
        }
    }
}
