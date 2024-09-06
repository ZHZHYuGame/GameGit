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
        // �첽����ģ��
        var request = UnityWebRequestAssetBundle.GetAssetBundle("file://" + modelPath);

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError("Error loading model: " + request.error);
        }
        else
        {
            // ��AssetBundle����ģ��
            var bundle = DownloadHandlerAssetBundle.GetContent(request);
            GameObject model = bundle.LoadAsset<GameObject>("MyModel");
            Instantiate(model, Vector3.zero, Quaternion.identity);

            // ��Ҫ����ж��AssetBundle���ͷ��ڴ�
            bundle.Unload(true);
        }
    }
}
