using System.IO;
using UnityEngine;
/// <summary>
/// ��ʼ������
/// </summary>
public class InitConfigProcedure : ProcedureBase
{
    private bool isConfigInitialized = false;

    protected override void OnEnter()
    {
        // ��ʼ�����ñ�
        InitConfigTables();
        Debug.Log(">>>>�������ó���");
    }

    protected override void OnUpdate()
    {
        if (!isConfigInitialized)
        {
            // ������ñ��ʼ���Ƿ����
            isConfigInitialized = CheckConfigTablesInitialized();
        }
        else
        {
            ProcedureManager.Instance.ChangeProcedure<CheckHotfixProcedure>();
        }
    }

    protected override void OnExit()
    {
        // �������ñ��ʼ�������Դ
        Debug.Log("�˳����ó���<<<<");
    }

    void InitConfigTables()
    {
        // ��ȡ�����ļ����������������ñ�
        string configPath = Application.dataPath + "/Config/config.json";
        if (File.Exists(configPath))
        {
            string configData = File.ReadAllText(configPath);
            // �����������ݲ��洢����Ϸ�����ö�����
        }
    }

    bool CheckConfigTablesInitialized()
    {
        // �ж����ñ��Ƿ��ʼ����ɵ��߼�
        return true;
    }
}