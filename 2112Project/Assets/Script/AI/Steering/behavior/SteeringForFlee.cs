using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 远离
/// </summary>
public class SteeringForFlee : Steering
{
    //离开的最大距离
    public float safeDistance = 10;
    public override Vector3 GetForce()
    {
        if (target == null) return Vector3.zero;
        var dir = transform.position - target.position;
        if (dir.magnitude < safeDistance)
        {
            //当前 vehicle.currentForce
            //期望 = （自身位置-目标位置）
            expectForce = (dir).normalized * speed;
            //实际 = （期望 - 当前） * 权重
            var realForce = (expectForce - vehicle.currentForce) * weight;
            return realForce;
        }
        else
        {
            return Vector3.zero;
        }
    }
}
