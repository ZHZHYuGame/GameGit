using UnityEngine;
/// <summary>
/// 检查热更
/// </summary>
public class CheckHotfixProcedure : ProcedureBase
{
    private bool isHotfixCompleted = false;

    protected override void OnEnter()
    {
        // 检查热更
        CheckForHotfix();
        Debug.Log(">>>>检查热更程序");
    }

    protected override void OnUpdate()
    {
        if (!isHotfixCompleted)
        {
            // 检查热更新状态
            isHotfixCompleted = CheckHotfixCompleted();
        }
        else
        {
            ProcedureManager.Instance.ChangeProcedure<InitGameCoreProcedure>();
        }
    }

    protected override void OnExit()
    {
        // 清理热更新相关资源
        Debug.Log("退出热更程序<<<<");
    }

    void CheckForHotfix()
    {
        VersionConfig.MakeVersionConfig();
        // 检查热更逻辑，与服务器通信比较版本号等
        if (!VersionConfig.CompareResourceLists(VersionConfig.ConfigFilePath, "new")) return;
        // 如果有热更，下载并安装热更文件
    }

    bool CheckHotfixCompleted()
    {
        // 判断热更新是否完成的逻辑
        return true;
    }
}