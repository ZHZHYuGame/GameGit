using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState: FSMState
{
    float speed;

    public ChaseState(FSMSystem fsmSystem, Transform playerTransform, Animator anim,float speed) : base(fsmSystem, playerTransform, anim)
    {
        stateID = StateID.Chase;
        this.speed = speed;
    }

    public override void Act(GameObject npc)
    {
        npc.transform.LookAt(playerTransform.position);
        npc.transform.Translate(Vector3.forward*speed*Time.deltaTime);
    }

    public override void Reason(GameObject npc)
    {
        if(Vector3.Distance(playerTransform.position,npc.transform.position)>6)
        {
            fsmSystem.PerformTransition(Transition.LostPlayer);
        }
        if(Vector3.Distance(playerTransform.position,npc.transform.position)<1)
        {
            fsmSystem.PerformTransition(Transition.AtkPlayer);
        }
    }
}
