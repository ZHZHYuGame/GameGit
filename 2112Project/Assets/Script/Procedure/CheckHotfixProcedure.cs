using UnityEngine;
/// <summary>
/// ����ȸ�
/// </summary>
public class CheckHotfixProcedure : ProcedureBase
{
    private bool isHotfixCompleted = false;

    protected override void OnEnter()
    {
        // ����ȸ�
        CheckForHotfix();
        Debug.Log(">>>>����ȸ�����");
    }

    protected override void OnUpdate()
    {
        if (!isHotfixCompleted)
        {
            // ����ȸ���״̬
            isHotfixCompleted = CheckHotfixCompleted();
        }
        else
        {
            ProcedureManager.Instance.ChangeProcedure<InitGameCoreProcedure>();
        }
    }

    protected override void OnExit()
    {
        // �����ȸ��������Դ
        Debug.Log("�˳��ȸ�����<<<<");
    }

    void CheckForHotfix()
    {
        // ����ȸ��߼����������ͨ�űȽϰ汾�ŵ�
        // ������ȸ������ز���װ�ȸ��ļ�
    }

    bool CheckHotfixCompleted()
    {
        // �ж��ȸ����Ƿ���ɵ��߼�
        return true;
    }
}