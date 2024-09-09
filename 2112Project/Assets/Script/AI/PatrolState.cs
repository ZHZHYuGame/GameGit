using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState:FSMState
{
    List<Transform> path = new List<Transform>();
    float speed;
    int index = 0;
    public PatrolState(FSMSystem fsmSystem,Transform playerTransform, Animator anim, float speed,Transform path) : base(fsmSystem, playerTransform,anim)
    {
        stateID = StateID.Patrol;
        this.speed = speed;
        Transform pathParent = path;
        Transform[] child = pathParent.GetComponentsInChildren<Transform>();
        foreach (Transform childChild in child)
        {
            if (pathParent!=childChild)
            {
                this.path.Add(childChild);
            }
        }
    }

    public override void Act(GameObject npc)
    {
        npc.transform.LookAt(path[index]);
        npc.transform.Translate(Vector3.forward*speed*Time.deltaTime);
        if (Vector3.Distance(path[index].position,npc.transform.position)<1)
        {
            index++;
            index%=path.Count;
        }
    }

    public override void Reason(GameObject npc)
    {
        if (Vector3.Distance(playerTransform.position, npc.transform.position) < 3)
        {
            fsmSystem.PerformTransition(Transition.SeePlayer);
        }
    }
}
