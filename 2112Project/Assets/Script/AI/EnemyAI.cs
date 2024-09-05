using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private FSMSystem fsm;
    private void Awake()
    {
        InitFSM();//³õÊ¼»¯×´Ì¬»ú
    }

    private void InitFSM()
    {
        fsm =new FSMSystem();
        FSMState patrolState = new PatrolState(fsm,3);
        patrolState.AddTransition(Transition.SeePlayer,StateID.Chase);
        FSMState chaseState = new ChaseState(fsm, 5);
        chaseState.AddTransition(Transition.LostPlayer, StateID.Patrol);
        fsm.AddState(patrolState);
        fsm.AddState(chaseState);
    }
    private void Update()
    {
        fsm.Update(this.gameObject);
    }
}
