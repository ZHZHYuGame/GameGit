using System.Collections.Generic;
using UnityEngine;
// ×´Ì¬IDÃ¶¾Ù
public enum StateID
{

    NullStateID = 0,//¿Õ
    Patrol,         //Ñ²Âß
    Chase           //×·Öð
}

// ×ª»»Ã¶¾Ù
public enum Transition
{
    NullTransition = 0,
    SeePlayer,
    LostPlayer
}

public abstract class FSMState
{
    protected StateID stateID;
    public StateID ID
    {
        get { return stateID; }
    }
    protected Animator anim;

    protected Dictionary<Transition, StateID> dic = new Dictionary<Transition, StateID>();

    protected FSMSystem fsmSystem;

    public FSMState(FSMSystem fsmSystem)
    {
        this.fsmSystem = fsmSystem;
    }
    public void AddTransition(Transition trans, StateID id)
    {
        dic.Add(trans, id);
    }
    public void DeleteTransition(Transition trans)
    {
        dic.Remove(trans);
    }

    public StateID GetOutputState(Transition trans)
    {
        if (dic.ContainsKey(trans))
        {
            return dic[trans];
        }
        return StateID.NullStateID;
    }

    public virtual void OnEnter() { }
    public virtual void OnExit() { }
    public abstract void Act(GameObject npc);
    public abstract void Reason(GameObject npc);
}