using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControlManager : MonoBehaviour
{
    public static SceneControlManager instance;//单例
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);//跳场景不销毁
        }
    }

    /// <summary>
    /// 跳转场景方法
    /// </summary>
    /// <param name="sceneName">场景名</param>
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneCourutine(sceneName));
    }
    /// <summary>
    /// 跳转场景携程
    /// </summary>
    /// <param name="sceneName">场景名</param>
    /// <returns></returns>
    IEnumerator LoadSceneCourutine(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)//判断加载是否完成
        {
            if (asyncLoad.progress >= 0.9f)//判断加载的进度
            {
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    //卸载场景的方法
    public void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }
}
