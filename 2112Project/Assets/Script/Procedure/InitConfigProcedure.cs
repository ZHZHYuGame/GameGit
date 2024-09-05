using System.IO;
using UnityEngine;
/// <summary>
/// 初始化配置
/// </summary>
public class InitConfigProcedure : ProcedureBase
{
    private bool isConfigInitialized = false;

    protected override void OnEnter()
    {
        // 初始化配置表
        InitConfigTables();
        Debug.Log(">>>>进入配置程序");
    }

    protected override void OnUpdate()
    {
        if (!isConfigInitialized)
        {
            // 检查配置表初始化是否完成
            isConfigInitialized = CheckConfigTablesInitialized();
        }
        else
        {
            ProcedureManager.Instance.ChangeProcedure<CheckHotfixProcedure>();
        }
    }

    protected override void OnExit()
    {
        // 清理配置表初始化相关资源
        Debug.Log("退出配置程序<<<<");
    }

    void InitConfigTables()
    {
        // 读取配置文件，解析并加载配置表
        string configPath = Application.dataPath + "/Config/config.json";
        if (File.Exists(configPath))
        {
            string configData = File.ReadAllText(configPath);
            // 解析配置数据并存储到游戏的配置对象中
        }
    }

    bool CheckConfigTablesInitialized()
    {
        // 判断配置表是否初始化完成的逻辑
        return true;
    }
}