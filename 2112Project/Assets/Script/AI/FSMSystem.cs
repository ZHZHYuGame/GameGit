using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMSystem
{
    Dictionary<StateID,FSMState> states = new Dictionary<StateID,FSMState>();
    FSMState currentState;
    public void Update(GameObject npc)
    {
        currentState.Act(npc);
        currentState.Reason(npc);
    }
    public void AddState(FSMState state)
    {
        if (currentState==null)
        {
            currentState = state;
        }
        states.Add(state.ID, state);
    }
    public void DeleteState(StateID id)
    {
        states.Remove(id);
    }
    public void PerformTransition(Transition trans)
    {
        StateID id = currentState.GetOutputState(trans);
        FSMState fSMState = states[id];
        currentState.DOAfterEntering();
        currentState = fSMState;
        currentState.DOBeforeEntering();
    }
}
