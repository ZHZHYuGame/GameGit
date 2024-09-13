using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 雷达
/// </summary>
[Serializable]
public class Radar//:MonoBehaviour
{
    #region 方法一:直接搜索范围内带标签的物体(可以不继承MonoBehaviour)
    public string neighbourTag = "neighbour";
    public float scanRadius = 10;
    //扫描周围的邻居
    public GameObject[] SanNeighbours(Vector3 selfPosition)
    {
        //查找物体(标签为 neighbour)的物体
        var array = GameObject.FindGameObjectsWithTag(neighbourTag);
        return Array.FindAll(array, go => Vector3.Distance(go.transform.position, selfPosition) < scanRadius);
    }
    #endregion
    /*
            #region 方法二:需要给雷达检测物体添加Collider组件，并需要挂载在物体上
            //碰撞体数组
            private Collider[] colliders;
            //计时器
            private float timer = 0;
            //邻居列表
            private List<GameObject> neighbors;
            private GameObject[] nbs;
            public GameObject[] Neighbors
            { 
                get 
                {
                    int index = 0;
                    foreach (var item in neighbors)
                    {
                        nbs[index++] = item;
                    }
                    return nbs; 
                } 
            }
            //检测的时间间隔
            public float checkTime = 0.3f;
            //设置邻域半径
            public float detectRadius = 10f;
            //设置检测哪一层游戏对象
            public LayerMask layerMask;
            //检测是否是一个类型的条件
            public string neighbourTagg = "neighbour";

            void Start()
            {
                //初始化邻居列表
                neighbors = new List<GameObject>();
            }
            void Update()
            {
                timer += Time.deltaTime;
                //如果距离上次检测的时间大于所设置的检测时间间隔，那么再次检测
                if (timer > checkTime)
                {
                    //清除邻居列表
                    neighbors.Clear();
                    //查找当前AI角色领域内的所有碰撞体
                    colliders = Physics.OverlapSphere(transform.position, detectRadius, layerMask);
                    for(int i = 0; i < colliders.Length; i++)
                    {//判断当前碰撞体的标签是否符合要求
                        if(colliders[i].tag == neighbourTagg)
                        {
                            neighbors.Add(colliders[i].gameObject);
                        }
                    }
                    //计时器归零
                    timer = 0;
                }
            }
            #endregion
    */
}