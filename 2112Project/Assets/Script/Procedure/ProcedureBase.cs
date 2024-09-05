using UnityEngine;
/// <summary>
/// �������̻���
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
        // ���������ʱִ��
    }

    protected virtual void OnUpdate()
    {
        // ÿ֡����ʱִ��
    }

    protected virtual void OnExit()
    {
        // �˳�������ʱִ��
    }
}