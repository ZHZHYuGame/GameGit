using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//追逐行为
public class SteeringForPursuit : Steering
{
    //画拦截点（调试）
    private Vector3 tempPiont;
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(tempPiont, 0.5f);
    }

    public override Vector3 GetForce()
    {
        //拦截 注意在 自己前方一定的角度内拦截，在侧边不拦截

        //1 目标和运动体的距离
        var toTarget = target.position - transform.position;
        var angle = Vector3.Angle(target.forward, toTarget);//计算角度
        if (angle > 20 && angle < 160)
        {
            //2 时间
            var targetSpeed = target.GetComponent<Vehicle>().currentForce.magnitude;
            var time = toTarget.magnitude / (targetSpeed + vehicle.currentForce.magnitude);
            //3 推断时间内走的距离
            var runDistance = targetSpeed * time;
            //4 拦截点位置
            var interceptPoint = target.position + target.forward * runDistance;
            tempPiont = interceptPoint;
            //5 期望（操控力）
            expectForce = (interceptPoint - transform.position).normalized * speed;
        }
        else
        {//靠近算法
            expectForce = toTarget.normalized * speed;
        }
        //6 实际 = （期望 - 当前）* 权重
        return (expectForce - vehicle.currentForce) * weight;
    }
}