using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Skilleditor : Editor
{
    [MenuItem("Tool/1.地形资源编辑器")]

    public static void Init()
    {
        SkillEditorWend skill=EditorWindow.GetWindow<SkillEditorWend>("地形资源编辑器");
    }
}
