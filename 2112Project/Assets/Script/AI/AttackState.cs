using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : FSMState
{
    public AttackState(FSMSystem fsmSystem, Transform playerTransform, Animator anim) : base(fsmSystem, playerTransform, anim)
    {
        stateID = StateID.Attack;
    }
    public override void DOBeforeEntering()
    {
        
    }
    
    public override void Act(GameObject npc)
    {
        Debug.Log(1);
    }

    public override void Reason(GameObject npc)
    {
        if (Vector3.Distance(npc.transform.position,playerTransform.position)>2)
        {
            fsmSystem.PerformTransition(Transition.SeePlayer);
        }
    }
}
