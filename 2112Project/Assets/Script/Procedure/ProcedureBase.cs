using UnityEngine;
/// <summary>
/// 程序流程基类
/// </summary>
public abstract class ProcedureBase
{
    public virtual void Enter()
    {
        OnEnter();
    }

    public virtual void Update()
    {
        OnUpdate();
    }

    public virtual void Exit()
    {
        OnExit();
    }

    protected virtual void OnEnter()
    {
        // 进入该流程时执行
    }

    protected virtual void OnUpdate()
    {
        // 每帧更新时执行
    }

    protected virtual void OnExit()
    {
        // 退出该流程时执行
    }
}