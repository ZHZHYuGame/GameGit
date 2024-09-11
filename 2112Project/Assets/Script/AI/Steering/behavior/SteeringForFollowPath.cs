using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SteeringForFollowPath : Steering
{
    public enum PartolMode
    {
        Once,
        Loop,
        Pingpong
    }
    //字段
    //路点
    public Transform[] WayPoints;//通过 属性窗口赋值 
                                 //当前路点 索引
    private int currentWPIndex = 0;
    //巡逻的模式
    public PartolMode partolMode;
    //巡逻到达的距离
    public float patrolArrivalDistance = 0.1f;//属性窗口赋值
                                              //方法
    public override Vector3 GetForce()
    {
        //是否到达当前路点
        if (Vector3.Distance(WayPoints[currentWPIndex].position, transform.position) < patrolArrivalDistance)
        {
            //是否是最后一个路点
            if (currentWPIndex == WayPoints.Length - 1)
            {
                //结束或者下一个路点
                switch (partolMode)
                {
                    case PartolMode.Once:
                        return Vector3.zero;
                    case PartolMode.Pingpong:
                        //反转巡逻点
                        Array.Reverse(WayPoints);
                        //currentWPIndex += 1;
                        break;
                }
            }
            //下一个路点
            currentWPIndex = (currentWPIndex + 1) % WayPoints.Length;
        }
        expectForce = (WayPoints[currentWPIndex].position - transform.position).normalized * speed;
        return (expectForce - vehicle.currentForce) * weight;
    }
}
