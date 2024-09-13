using UnityEngine;
abstract public class Steering : MonoBehaviour
{
    /// <summary>
    /// 期望操控力
    /// </summary>
    [HideInInspector]
    public Vector3 expectForce;
    /// <summary>
    /// 当前的运动对象（抽象为质点）
    /// </summary>
    [HideInInspector]
    public Vehicle vehicle;
    /// <summary>
    /// 目标
    /// </summary>
    /// 通过监视【属性】窗口赋值
    /// 也可以通过代码赋值
    public Transform target;
    /// <summary>
    /// 速度
    /// </summary>
    public float speed = 5;
    /// <summary>
    /// 表示每个操控力的权重
    /// </summary>
    public int weight = 1;
    /// <summary>
    /// 计算各个操控行为对当前物体的影响 实际操控力
    /// </summary>
    abstract public Vector3 GetForce();
    //初始化
    protected void Start()
    {
        vehicle = GetComponent<Vehicle>();
        if (speed == 0) speed = vehicle.maxSpeed;
    }
}