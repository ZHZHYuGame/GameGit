using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionController : Vehicle
{
    //方法需要反复调用 需要持续判断当前运动
    void Update()
    {
        Rotation();
        Movement();
        PlayAnimation();
    }
    /// <summary>
    /// 转向:当前的操控方向
    /// </summary>
    public void Rotation()
    {
        if (currentForce != Vector3.zero)
        {
            //获得当前操控力的方向的值
            var dir = Quaternion.LookRotation(currentForce);
            //（如果不希望角色y轴旋转）固定y轴
            //var dir = Quaternion.LookRotation(new Vector3(currentForce.x,0,currentForce.z));
            //得到插值 圆滑的旋转
            transform.rotation = Quaternion.Lerp(transform.rotation, dir, rotationSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// 移动
    /// </summary>
    public void Movement()
    {
        //1 计算出当前操控方向和速度
        currentForce += finalForce * Time.deltaTime;
        currentForce = Vector3.ClampMagnitude(currentForce, maxSpeed);
        //2 移动
        transform.position += currentForce * Time.deltaTime;
        //不希望角色起飞(固定y轴)
        //transform.position += new Vector3((currentForce * Time.deltaTime).x,0, (currentForce * Time.deltaTime).z);
    }
    /// <summary>
    /// 播放动画
    /// </summary>
    public void PlayAnimation() { }
}
