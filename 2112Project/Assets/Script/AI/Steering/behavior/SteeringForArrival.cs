using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 抵达
/// </summary>
public class SteeringForArrival : Steering
{
    public float slowdownDistance = 5;//减速区半径
    public float arrivalDistance = 2;//到达区半径
                                     //得到计算合力
    public override Vector3 GetForce()
    {
        float distance = Vector3.Distance(target.position, transform.position) - arrivalDistance;
        //减速区外:靠近算法  保持在最高速
        float realSpeed = speed;
        //减速区内:速度为0
        if (distance <= 0)
            return Vector3.zero;
        //减速区： 速度递减
        if (distance < slowdownDistance)
        {
            realSpeed = distance / (slowdownDistance - arrivalDistance) * speed;
            realSpeed = realSpeed < 1 ? 1 : realSpeed;
        }
        expectForce = (target.position - transform.position).normalized * realSpeed;
        return (expectForce - vehicle.currentForce) * weight;
        //到达区内
    }
}
