using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    FSMSystem fsmSystem;
    Animator anim;
    private void Awake()
    {
        InitFSM();
    }

    private void InitFSM()
    {
        anim = GetComponent<Animator>();
        fsmSystem = new FSMSystem();
        Transform playerTransform = GameObject.Find("Player").transform;
        ChaseState chaseState = new ChaseState(fsmSystem, playerTransform,anim, 1.5f);
        chaseState.AddTransition(Transition.LostPlayer,StateID.Sleep);
        chaseState.AddTransition(Transition.AtkPlayer, StateID.Attack);

        //PatrolState patrolState = new PatrolState(fsmSystem,playerTransform, 3, GameObject.Find("Path").transform);
        //patrolState.AddTransition(Transition.SeePlayer, StateID.Chase);

        SleepState sleepState = new SleepState(fsmSystem, playerTransform, anim, transform.position);
        sleepState.AddTransition(Transition.SeePlayer,StateID.Chase);

        AttackState attackState = new AttackState(fsmSystem, playerTransform, anim);
        attackState.AddTransition(Transition.SeePlayer, StateID.Chase);


        fsmSystem.AddState(chaseState);
        fsmSystem.AddState(sleepState);
        fsmSystem.AddState(attackState);
    }

    // Update is called once per frame
    void Update()
    {
        fsmSystem.Update(this.gameObject);
    }
}
