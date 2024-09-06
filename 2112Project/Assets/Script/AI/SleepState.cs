using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepState : FSMState
{
    Vector3 origin;
    public SleepState(FSMSystem fsmSystem,Transform playerTransform, Animator anim,Vector3 origin) : base(fsmSystem, playerTransform,anim)
    {
        stateID = StateID.Sleep;
        this.origin = origin;
    }

    public override void Act(GameObject npc)
    {
        if (Vector3.Distance(npc.transform.position, origin) < 1) return;
        npc.transform.LookAt(origin);
        npc.transform.Translate(Vector3.forward * 5 * Time.deltaTime);
    }

    public override void Reason(GameObject npc)
    {
        if(Vector3.Distance(playerTransform.position,npc.transform.position)<4)
        {
            fsmSystem.PerformTransition(Transition.SeePlayer);
        }
    }
}
