using System.IO;
using UnityEngine;
public class ChaseState : FSMState
{
    private Transform playerTrans;
    private float speed;

    public ChaseState(FSMSystem fsmSystem,float speed) : base(fsmSystem)
    {
        stateID = StateID.Chase;
        this.speed = speed;
        playerTrans = GameObject.Find("Player").transform;
    }
    public override void OnEnter()
    {
        base.OnEnter();
    }
    public override void OnExit()
    {
        base.OnExit();
    }
    // ×·Öð
    public override void Act(GameObject npc)
    {
        npc.transform.LookAt(playerTrans.position);
        npc.transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    // ÇÐ»»
    public override void Reason(GameObject npc)
    {
        if (Vector3.Distance(playerTrans.position, npc.transform.position) > 6)
        {
            fsmSystem.PerformTransition(Transition.LostPlayer);
        }
    }
}