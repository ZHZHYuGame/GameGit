using UnityEngine;
/// <summary>
/// 
/// </summary>
public class InitGameCoreProcedure : ProcedureBase
{
    private bool isGameCoreInitialized = false;

    protected override void OnEnter()
    {
        // ��ʼ����Ϸ����
        InitGameCore();
        Debug.Log(">>>>������Ϸ���ĳ���");
    }

    protected override void OnUpdate()
    {
        if (!isGameCoreInitialized)
        {
            // �����Ϸ���ĳ�ʼ���Ƿ����
            isGameCoreInitialized = CheckGameCoreInitialized();
        }
        else
        {
            ProcedureManager.Instance.ChangeProcedure<LoadMainMenuProcedure>();
        }
    }

    protected override void OnExit()
    {
        // ������Ϸ���ĳ�ʼ�������Դ
        Debug.Log("�˳���Ϸ���ĳ���<<<<");
    }

    void InitGameCore()
    {
        // ��ʼ����Ϸ�ĸ���ϵͳ�͹������
    }

    bool CheckGameCoreInitialized()
    {
        // �ж���Ϸ���ĳ�ʼ���Ƿ���ɵ��߼�
        return true;
    }
}