using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PatrolState : FSMState
{
    private List<Vector3> path;
    private int index = 0;
    private Transform playerTrans;
    private float speed;
    public PatrolState(FSMSystem fsmSystem, float speed) : base(fsmSystem)
    {
        stateID = StateID.Patrol;
        GetRandowPathList();
        this.speed = speed;
        playerTrans = GameObject.Find("Player").transform;
    }
    // Ëæ»úÂ·¾¶µã
    private void GetRandowPathList()
    {
        path = new List <Vector3>();
        for (int i = 0; i < 8; i++)
        {
            int x = Random.Range(-10, 10);
            int z = Random.Range(-10, 10);
            Vector3 randomPoint = new Vector3(x, 0, z);
            path.Add(randomPoint);
        }
    }
    //½øÈë×´Ì¬²¥¶¯»­
    public override void OnEnter()
    {
        base.OnEnter();
    }
    public override void OnExit()
    {
        base.OnExit();
    }
    // Ñ²Âß
    public override void Act(GameObject npc)
    {
        npc.transform.LookAt(path[index]);
        npc.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (Vector3.Distance(npc.transform.position, path[index])<1)
        {
            index++;
            index %= path.Count;
        }
    }
    // ÇÐ»»
    public override void Reason(GameObject npc)
    {
        if(Vector3.Distance(playerTrans.position,npc.transform.position)<3)
        {
            fsmSystem.PerformTransition(Transition.SeePlayer);
        }
    }
}
