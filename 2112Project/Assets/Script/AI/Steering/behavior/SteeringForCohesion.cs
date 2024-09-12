using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 队列
/// </summary>
public class SteeringForCohesion : Steering
{
    //雷达
    public Radar radar = new Radar();

    public override Vector3 GetForce()
    {
        //通过雷达扫描得到周围邻居
        var allNeighbour = radar.SanNeighbours(transform.position);
        //当前AI角色的邻居平均朝向
        Vector3 averageDirection = new Vector3(0, 0, 0);
        for (int i = 0; i < allNeighbour.Length; i++)
        {//朝向向量进行累加
            averageDirection += allNeighbour[0].transform.forward;
        }
        //将累加得到的朝向向量除以邻居的个数，求出平均朝向向量
        averageDirection /= (float)allNeighbour.Length;
        //平均朝向向量减去当前朝向向量，得到操控向量
        averageDirection -= transform.forward;

        return averageDirection;
    }
}