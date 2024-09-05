using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControlManager : MonoBehaviour
{
    public static SceneControlManager instance;//����
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);//������������
        }
    }

    /// <summary>
    /// ��ת��������
    /// </summary>
    /// <param name="sceneName">������</param>
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneCourutine(sceneName));
    }
    /// <summary>
    /// ��ת����Я��
    /// </summary>
    /// <param name="sceneName">������</param>
    /// <returns></returns>
    IEnumerator LoadSceneCourutine(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)//�жϼ����Ƿ����
        {
            if (asyncLoad.progress >= 0.9f)//�жϼ��صĽ���
            {
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    //ж�س����ķ���
    public void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }
}
