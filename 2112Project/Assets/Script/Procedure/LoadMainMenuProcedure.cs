using UnityEngine;
/// <summary>
/// 主场景流程
/// </summary>
public class LoadMainMenuProcedure : ProcedureBase
{
    protected override void OnEnter()
    {
        // 加载主菜单场景
        LoadMainMenu();
        Debug.Log(">>>>进入主菜单程序");
    }

    protected override void OnUpdate()
    {
        // 主菜单加载完成后可以进行一些额外的初始化或等待玩家输入
    }

    protected override void OnExit()
    {
        // 清理主菜单加载相关资源
        Debug.Log("退出主菜单程序<<<<");
    }

    void LoadMainMenu()
    {
        // 加载游戏主界面场景
    }
}