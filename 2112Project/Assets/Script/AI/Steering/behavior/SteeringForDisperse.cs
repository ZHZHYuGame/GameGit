using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringForDisperse : Steering
{
    //字段
    //雷达
    public Radar radar = new Radar();
    //与中心点距离最近的距离
    public float nearDistance;
    //方法
    public override Vector3 GetForce()
    {
        //通过雷达扫描得到周围邻居，算出  与所有邻居产生的排斥力总和
        var allNeighbour = radar.SanNeighbours(transform.position);
        expectForce = Vector3.zero;
        //算出  与所有邻居产生的排斥力总和
        for (int i = 0; i < allNeighbour.Length; i++)
        {
            var dir = transform.position - allNeighbour[i].transform.position;
            if (dir.magnitude < nearDistance && gameObject != allNeighbour[i].gameObject)
            {
                expectForce += dir.normalized;
            }

        }
        if (expectForce == Vector3.zero) return Vector3.zero;

        expectForce = expectForce.normalized * speed;
        return (expectForce - vehicle.currentForce) * weight;
    }
}
