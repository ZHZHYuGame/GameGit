using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 运动体类
/// </summary>
public class Vehicle : MonoBehaviour
{
    /// <summary>
    /// 当前操控力:向量
    /// </summary>
    [HideInInspector]
    public Vector3 currentForce;
    /// <summary>
    /// 最终的合力
    /// </summary>
    [HideInInspector]
    public Vector3 finalForce;
    /// <summary>
    /// 当前角色所具有的操控行为类数组
    /// </summary>
    [HideInInspector]
    public Steering[] steerings;
    /// <summary>
    /// 最大速度
    /// </summary>
    public float maxSpeed = 10;
    /// <summary>
    /// 转向速度
    /// </summary>
    public float rotationSpeed = 5;
    /// <summary>
    /// 质量
    /// </summary>
    public float mass = 1;
    /// <summary>
    /// 合力上限
    /// </summary>
    public float maxForce = 100;
    /// <summary>
    /// 操控力的计算间隔时间，为了达到更高的帧率，操控力不需要每帧更新
    /// </summary>
    public float computerInternal = 0.2f;
    /// <summary>
    /// 是否在二维平面上，如果是计算GameObject的距离时，忽略y值的不同
    /// </summary>
    public bool isPlane = false;

    //初始化 得到当前这个AI角色身上所有属于操控行为类的列表
    void Start()
    {
        steerings = GetComponents<Steering>();
    }
    /// <summary>
    /// 计算出实际操控力:把当前所有影响对象力进行计算 求合力
    /// 同时考虑质量等问题  返回最终的实际操控力
    /// </summary>
    public void ComputeFinalForce()
    {
        //每次计算最终合力 让合力归零
        finalForce = Vector3.zero;
        //1 求当前对象的实际操控力 也就是所有对当前对象产生影响的力 计算合力
        for (int i = 0; i < steerings.Length; i++)
        {
            if (steerings[i].enabled)
            {
                finalForce += steerings[i].GetForce();
            }
        }
        if (finalForce == Vector3.zero)
            currentForce = Vector3.zero;
        //2 是否是平面（忽略y轴影响）
        if (isPlane)
            finalForce.y = 0;
        //把向量限制在一个特定的长度，不超过合力上限
        finalForce = Vector3.ClampMagnitude(finalForce, maxForce);
        //考虑质量最最终合力的影响  合力=合力/质量
        finalForce /= mass;
    }
    //启用方法
    private void OnEnable()
    {
        InvokeRepeating("ComputeFinalForce", 0, computerInternal);
    }
    //结束时取消调用函数
    private void OnDisable()
    {
        CancelInvoke("ComputeFinalForce");
    }
}