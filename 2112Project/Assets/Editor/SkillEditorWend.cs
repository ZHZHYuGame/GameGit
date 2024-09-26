using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public enum MapType
{
    None,
    �λ����,
    �＾ĺɫ,
    ����֮·,
    ��ϼ��Ļ,
}
public enum EnemyType 
{
    None,
    A��ʽֱ��Ѳ��,
    B��ʽ����Ѳ��,
    C��ʽ��������,
}
public enum Difficulty
{
    None,
    ��,
    ����,
    ج��,
}
public class SkillEditorWend : EditorWindow
{
    MapType maptype = new MapType();
    EnemyType enemy = new EnemyType();
    Difficulty difficulty=new Difficulty();
    string maptypestr,enemystr,difficultystr;
    Vector3 pos;
    bool indexprop;
    TerrainData terrainpass;
    public void OnGUI()
    {
        EditorGUILayout.LabelField("��ѡ�񳡾���");
        maptype =(MapType)EditorGUILayout.EnumPopup(maptype);
        maptypestr=maptype.ToString();
        EditorGUILayout.LabelField("��ѡ��������ɵ�λ���Լ�Ѳ��·�ߣ�");
        enemy = (EnemyType)EditorGUILayout.EnumPopup(enemy);
        enemystr=enemy.ToString();
        EditorGUILayout.LabelField("��ѡ�񸱱����Ѷȣ�");
        difficulty = (Difficulty)EditorGUILayout.EnumPopup(difficulty);
        difficultystr=difficulty.ToString();
        pos = EditorGUILayout.Vector3Field("������λ��(��������:������X�ķ�Χ(210~220),������YΪ��,������Z�ķ�Χ(-40~-70))��", pos);
        EditorGUILayout.LabelField("�Ƿ������ڵ��ߣ�");
        if (GUILayout.Button("��/��"))
        {
            if(indexprop)
            {
                indexprop = false;
            }
            else
            {
                indexprop = true;
            }
        }
        GUILayout.Space(40);
        if (GUILayout.Button("ȷ��"))
        {
            terrainpass = new TerrainData();
            terrainpass.maptype = maptypestr;
            terrainpass.enemytype = enemystr;
            terrainpass.difficultytype = difficultystr;
            terrainpass.pos = pos;
            terrainpass.indexprop = indexprop;

            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };
            string str = JsonConvert.SerializeObject(terrainpass, settings);
            File.WriteAllText(Application.dataPath + "/Resources/terrain.json", str);
            AssetDatabase.Refresh();

        }
    }
}
