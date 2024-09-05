using UnityEngine;
/// <summary>
/// 程序流程管理
/// </summary>
public class ProcedureManager : Singleton<ProcedureManager>
{
    private ProcedureBase currentProcedure;

    public void ChangeProcedure<T>() where T : ProcedureBase, new()
    {
        if (currentProcedure != null)
        {
            currentProcedure.Exit();
        }

        T newProcedure = new T();
        currentProcedure = newProcedure;
        currentProcedure.Enter();
    }

    private void Update()
    {
        if (currentProcedure != null)
        {
            currentProcedure.Update();
        }
    }
}