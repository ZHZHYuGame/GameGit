using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Skilleditor : Editor
{
    [MenuItem("Tool/1.������Դ�༭��")]

    public static void Init()
    {
        SkillEditorWend skill=EditorWindow.GetWindow<SkillEditorWend>("������Դ�༭��");
    }
}
