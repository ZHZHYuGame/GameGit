using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 徘徊
/// </summary>
public class SteeringForWander : Steering
{
    //字段
    //运动体与徘徊圆圆心的距离
    public float wanderDistance = 10;//取值重要，可以修改，合力范围之内
                                     //徘徊圆半径
    public float wanderRadius = 15;
    //最大偏移值
    public float maxOffset = 200;
    //改变目标的时间间隔
    public float changeTargetInterval = 3;
    //徘徊圆圆周上的目标点
    private Vector3 circleTarget;//(相对圆心)
                                 //目标点
    private Vector3 targetPos;//（相对运动体）

    //方法
    //需要设置初始目标点(在徘徊圆周上)
    private new void Start()
    {
        base.Start();//
                     //需要设置初始目标点(在徘徊圆周上)
        circleTarget = new Vector3(wanderRadius, 0, 0);
        //不断改变目标点在徘徊圆周上)
        InvokeRepeating("ChangeTarget", 0, changeTargetInterval);
    }
    public void ChangeTarget()
    {
        //根据当前目标点，随机 生成偏移位置
        var offSetPosition = circleTarget + new Vector3(Random.Range(-maxOffset, maxOffset), Random.Range(-maxOffset, maxOffset), Random.Range(-maxOffset, maxOffset));
        //将偏移后的位置  投射到圆周（位置相对于圆心）
        circleTarget = offSetPosition.normalized * wanderRadius;
        //位置再折算成世界坐标
        targetPos = transform.position +
            transform.forward * wanderDistance + circleTarget;
    }
    public override Vector3 GetForce()
    {
        expectForce = (targetPos - transform.position).normalized * speed;
        return (expectForce - vehicle.currentForce) * weight;
    }
    //看看徘徊圆，调试
    void OnDrawGizmos()
    {
        //前方徘徊圆
        var sphereCenter = transform.position + transform.forward * wanderDistance;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(sphereCenter, wanderRadius);
        //画目标点
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(targetPos, 1);
        //画线
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, sphereCenter);
    }
}