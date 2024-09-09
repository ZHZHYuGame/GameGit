using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum StateID
{
    NullState = 0,
    Patrol,
    Chase,
    Sleep,
    Attack,
}
public enum Transition
{
    NullTransition = 0,
    SeePlayer,
    LostPlayer,
    AtkPlayer
}
public abstract class FSMState
{
    protected StateID stateID;
    public StateID ID{ get { return stateID; } }
    protected FSMSystem fsmSystem;
    protected Transform playerTransform;
    protected Animator anim;
    public FSMState(FSMSystem fsmSystem, Transform playerTransform, Animator anim)
    {
        this.fsmSystem = fsmSystem;
        this.playerTransform = playerTransform;
        this.anim = anim;
    }

    Dictionary<Transition, StateID> dic = new Dictionary<Transition, StateID>();
    public void AddTransition(Transition trans,StateID id)
    {
        dic.Add(trans, id);
    }
    public void RemoveTransition(Transition trans) 
    {
        dic.Remove(trans);
    }
    public StateID GetOutputState(Transition trans)
    {
        if (dic.ContainsKey(trans))
        {
            return dic[trans];
        }
        return StateID.NullState;
    }
    public virtual void DOBeforeEntering() { }
    public virtual void DOAfterEntering() { }
    public abstract void Act(GameObject npc);
    public abstract void Reason(GameObject npc);
}
