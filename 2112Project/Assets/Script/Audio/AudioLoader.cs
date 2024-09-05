using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AudioLoader : MonoBehaviour
{
    public static AudioLoader instance;//µ¥Àý
    private string assetBundlesUrl = "file:///" + Application.streamingAssetsPath + "/AssetBundles/";
    private AudioManager audioManager;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        audioManager = AudioManager.instance;
    }

    public void LoadMusic(string musicName)
    {
        StartCoroutine(LoadAudioClip(musicName, audioManager.PlayMusic));
    }

    public void LoadSound(string soundName)
    {
        StartCoroutine(LoadAudioClip(soundName, audioManager.PlaySound));
    }

    IEnumerator LoadAudioClip(string clipName, System.Action<AudioClip> callback)
    {
        string path = assetBundlesUrl + "Audio/" + clipName + ".ab";
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(path);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            AssetBundle ab = DownloadHandlerAssetBundle.GetContent(www);
            AudioClip audioClip = ab.LoadAsset<AudioClip>(clipName);
            ab.Unload(false);
            callback(audioClip);
        }
        else
        {
            Debug.LogError("Error loading asset: " + www.error);
        }
    }
}
