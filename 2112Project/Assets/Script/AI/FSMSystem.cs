using System.Collections.Generic;
using UnityEngine;

public class FSMSystem
{
    private Dictionary<StateID, FSMState> states = new Dictionary<StateID, FSMState>();
    private FSMState currentState;
    // Ö´ÐÐ
    public void Update(GameObject npc)
    {
        currentState.Act(npc); 
        currentState.Reason(npc);
    }
    // Ìí¼Ó×´Ì¬
    public void AddState(FSMState state)
    {
        if (currentState == null)
        {
            currentState = state;
        }
        states.Add(state.ID, state);
    }
    // É¾³ý×´Ì¬
    public void DeleteState(StateID id)
    {
        states.Remove(id);
    }

    // Ö´ÐÐ×´Ì¬×ª»»µÄ·½·¨
    public void PerformTransition(Transition trans)
    {
        StateID id = currentState.GetOutputState(trans);
        FSMState fSMState = states[id];
        currentState.OnExit();
        currentState = fSMState;
        currentState.OnEnter();
    }
}