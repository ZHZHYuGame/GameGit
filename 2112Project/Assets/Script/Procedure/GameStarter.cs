using UnityEngine;
/// <summary>
/// ��Ϸ����
/// </summary>
public class GameStarter : MonoBehaviour
{
    private void Start()
    {
        ProcedureManager.Instance.ChangeProcedure<SplashProcedure>();
        
    }
}