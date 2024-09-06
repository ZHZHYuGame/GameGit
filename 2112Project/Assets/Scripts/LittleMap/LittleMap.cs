using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;



public class LittleMap : MonoBehaviour
{
    public NavMeshAgent agent; // 确保在Inspector中将你的NavMeshAgent组件拖到这个字段
    public Image Map;
    public Image player;

    private float _width = 118;
    private float _height = 118;

    //设置一个小地图上的图片加载容器;
    //小地图的存储的UI的实体,全部设置为地图的子对象;
    //加载地图头像的流程在小地图类内进行;
    //readonly Dictionary<int, HeadImageInMap> entities = new();

    public void Awake()
    {
        //根据游戏全局管理判断是否需要小地图;


        //如果需要/
        //进行事件绑定
        // 根据事件的回调进行UI实体小地图的绑定;
    }


    #region  模拟玩家移动模块
    void Update()
    {
        // 检查鼠标左键是否按下
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // 尝试从鼠标位置射出一条射线，看它是否击中了NavMesh
            if (Physics.Raycast(ray, out hit))
            {
                // 如果射线击中了可行走的表面，就设置NavMeshAgent的目标位置
                agent.SetDestination(hit.point);
            }
        }
        SetPlayerPositionInMap(agent.transform.position);
    }
    #endregion


    /// <summary>
    /// 玩家移动的时候直接调用小地图的方法就可以了,传入玩家的世界坐标;这里要考虑敌人的位置
    /// </summary>
    /// <param name="PlayerPosition"></param>
    /// <param name="PlayerRotation">四元数旋转</param>
    void SetPlayerPositionInMap(Vector3 playerPosition)
    {
        var x = playerPosition.x;
        var y = playerPosition.z;
        var uix = (x + _width / 2) / _width;
        var uiy = (y + _height / 2) / _height;
        //1.Pivot点和父对象大小之间的关系;
        //2.中心点不动移动子对象;新的中心点换算
        if ((uiy < 0.25 && uix < 0.75 && uix > 0.25) || (uix > 0.75 && uiy > 0.25 && uiy < 0.75f) || (uix < 0.25 && uiy < 0.75 && uiy > 0.25) || (uiy > 0.75 && uix > 0.25 && uix < 0.75f))
        {
            if (uiy < 0.25)
            {
                Map.rectTransform.pivot = new Vector2(uix, 0.25f);
            }
            else if (uiy > 0.75)
            {
                Map.rectTransform.pivot = new Vector2(uix, 0.75f);
            }
            else if (uix < 0.25)
            {
                Map.rectTransform.pivot = new Vector2(0.25f, uiy);
            }
            else if (uix > 0.75f)
            {
                Map.rectTransform.pivot = new Vector2(0.75f, uiy);
            }
            UpdatePanel(playerPosition);
        }
        else if (uix < 0.25f || uix > 0.75f || uiy < 0.25f || uiy > 0.75f)
        {
            UpdatePanel(playerPosition);
        }
        else
        {
            player.rectTransform.localPosition = new Vector2(0, 0);
            Map.rectTransform.pivot = new Vector2(uix, uiy);
        }
    }


    void UpdatePanel(Vector3 playerPosition)
    {
        var value = Map.rectTransform.pivot;
        //模拟地图中心点变化;
        var relationX = _width * (value.x - 1.0f / 2);
        var relationY = _height * (value.y - 1.0f / 2);

        //中心点映射的地图的位置;以此为原点;
        var zero = new Vector3(relationX, 1, relationY);

        //=>玩家坐标-zero就是以zero为坐标系的相对坐标;
        var relationVec = playerPosition - zero;
        //映射到小地图上;
        var localPlayerx = relationVec.x * Map.rectTransform.sizeDelta.x / _width;
        var localPlayery = relationVec.z * Map.rectTransform.sizeDelta.y / _height;
        //有用的前提是中心点在中间
        var local = new Vector2(localPlayerx, localPlayery);
        var isPoint = Map.rectTransform.TransformPoint(local);
        var pos = RectTransformUtility.WorldToScreenPoint(null, isPoint);
        player.rectTransform.position = pos;
    }

}
