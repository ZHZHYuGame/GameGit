using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

class CreateSceneUtility
{
    public static void CreateScene(string sceneName, Action delegateToExecute)
    {
#if UNITY_EDITOR
        string scenePath = "Assets/" + sceneName + ".unity";
        var initScene = SceneManager.GetActiveScene();
        var list = UnityEditor.EditorBuildSettings.scenes.ToList();
        var newScene = UnityEditor.SceneManagement.EditorSceneManager.NewScene(UnityEditor.SceneManagement.NewSceneSetup.DefaultGameObjects, UnityEditor.SceneManagement.NewSceneMode.Additive);
        GameObject.DestroyImmediate(Camera.main.GetComponent<AudioListener>());
        delegateToExecute();
        UnityEditor.SceneManagement.EditorSceneManager.SaveScene(newScene, scenePath);
<<<<<<< HEAD
=======
        UnityEditor.SceneManagement.EditorSceneManager.UnloadSceneAsync(newScene);
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b

        list.Add(new UnityEditor.EditorBuildSettingsScene(scenePath, true));
        UnityEditor.EditorBuildSettings.scenes = list.ToArray();
        SceneManager.SetActiveScene(initScene);
#endif
    }
}
