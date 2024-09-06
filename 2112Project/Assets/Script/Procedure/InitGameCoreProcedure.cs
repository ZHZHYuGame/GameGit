using UnityEngine;
/// <summary>
/// 
/// </summary>
public class InitGameCoreProcedure : ProcedureBase
{
    private bool isGameCoreInitialized = false;

    protected override void OnEnter()
    {
        // 初始化游戏核心
        InitGameCore();
        Debug.Log(">>>>进入游戏核心程序");
    }

    protected override void OnUpdate()
    {
        if (!isGameCoreInitialized)
        {
            // 检查游戏核心初始化是否完成
            isGameCoreInitialized = CheckGameCoreInitialized();
        }
        else
        {
            ProcedureManager.Instance.ChangeProcedure<LoadMainMenuProcedure>();
        }
    }

    protected override void OnExit()
    {
        // 清理游戏核心初始化相关资源
        Debug.Log("退出游戏核心程序<<<<");
    }

    void InitGameCore()
    {
        // 初始化游戏的各个系统和管理对象
    }

    bool CheckGameCoreInitialized()
    {
        // 判断游戏核心初始化是否完成的逻辑
        return true;
    }
}