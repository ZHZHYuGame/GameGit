using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringForEvadeObstacle : Steering
{
    //字段
    //障碍物标记
    public string obstacleTag = "obstacle";
    //探头的位置
    public Transform probePos;
    //探头的长度
    public float probeLength = 15;
    //最小推力
    public float minPushForce = 30;
    //方法
    public override Vector3 GetForce()
    {
        //使用探头检测前方，如果有障碍物
        RaycastHit hit;
        //bool b1 = Physics.Raycast(probePos.position, probePos.forward,
        //    out hit, probeLength);
        //bool b2 = false;
        //if (hit.collider != null)
        //{
        //    b2 = hit.collider.tag == obstacleTag;
        //}
        if (Physics.Raycast(probePos.position, probePos.forward,
            out hit, probeLength) && hit.collider.tag == obstacleTag)
        {
            //由障碍物向碰撞点 产生一个推力，这个推力就是操控力
            expectForce = hit.point - hit.transform.position;
            //操控力 如果很小  则将操控力放大
            if (expectForce.magnitude < minPushForce)
            {
                expectForce = expectForce.normalized * minPushForce;
            }
            //返回操控力 
            return expectForce * weight;
        }
        //没有障碍物
        return Vector3.zero;
    }
}
