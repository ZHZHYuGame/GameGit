using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public enum MapType
{
    None,
    梦幻晚会,
    秋季暮色,
    宇宙之路,
    晚霞落幕,
}
public enum EnemyType 
{
    None,
    A方式直线巡逻,
    B方式四周巡逻,
    C方式靠近出怪,
}
public enum Difficulty
{
    None,
    简单,
    困难,
    噩梦,
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
        EditorGUILayout.LabelField("请选择场景：");
        maptype =(MapType)EditorGUILayout.EnumPopup(maptype);
        maptypestr=maptype.ToString();
        EditorGUILayout.LabelField("请选择怪物生成的位置以及巡逻路线：");
        enemy = (EnemyType)EditorGUILayout.EnumPopup(enemy);
        enemystr=enemy.ToString();
        EditorGUILayout.LabelField("请选择副本的难度：");
        difficulty = (Difficulty)EditorGUILayout.EnumPopup(difficulty);
        difficultystr=difficulty.ToString();
        pos = EditorGUILayout.Vector3Field("传送阵位置(友情提醒:传送阵X的范围(210~220),传送阵Y为零,传送阵Z的范围(-40~-70))：", pos);
        EditorGUILayout.LabelField("是否开启场内道具：");
        if (GUILayout.Button("开/否"))
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
        if (GUILayout.Button("确定"))
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
