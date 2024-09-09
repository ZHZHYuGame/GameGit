using UnityEngine;
/// <summary>
/// ”Œœ∑∆Ù∂Ø
/// </summary>
public class GameStarter : MonoBehaviour
{
    private void Start()
    {
        ProcedureManager.Instance.ChangeProcedure<SplashProcedure>();
        
    }
}