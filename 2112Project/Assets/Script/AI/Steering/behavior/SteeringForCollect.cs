using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringForCollect : Steering
{
    //字段
    //雷达
    public Radar radar = new Radar();
    //与中心点距离最近的距离
    public float nearDistance;
    //方法

    public override Vector3 GetForce()
    {
        //通过雷达扫描得到周围邻居，算出所有邻居中心点
        var allNeighbour = radar.SanNeighbours(transform.position);
        var center = Vector3.zero;
        for (int i = 0; i < allNeighbour.Length; i++)
        {
            center += allNeighbour[i].transform.position;
        }
        center = center / allNeighbour.Length;
        //向中心靠近
        if (Vector3.Distance(center, transform.position) > nearDistance)
        {
            expectForce = (center - transform.position).normalized * speed;
            return (expectForce - vehicle.currentForce) * weight;
        }
        return Vector3.zero;
    }
}
