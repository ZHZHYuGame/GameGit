using UnityEngine;
/// <summary>
/// ����������
/// </summary>
public class LoadMainMenuProcedure : ProcedureBase
{
    protected override void OnEnter()
    {
        // �������˵�����
        LoadMainMenu();
        Debug.Log(">>>>�������˵�����");
    }

    protected override void OnUpdate()
    {
        // ���˵�������ɺ���Խ���һЩ����ĳ�ʼ����ȴ��������
    }

    protected override void OnExit()
    {
        // �������˵����������Դ
        Debug.Log("�˳����˵�����<<<<");
    }

    void LoadMainMenu()
    {
        // ������Ϸ�����泡��
    }
}