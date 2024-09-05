using System.Collections.Generic;
using UnityEngine;

public class FSMSystem
{
    private Dictionary<StateID, FSMState> states = new Dictionary<StateID, FSMState>();
    private FSMState currentState;
    // ִ��
    public void Update(GameObject npc)
    {
        currentState.Act(npc); 
        currentState.Reason(npc);
    }
    // ���״̬
    public void AddState(FSMState state)
    {
        if (currentState == null)
        {
            currentState = state;
        }
        states.Add(state.ID, state);
    }
    // ɾ��״̬
    public void DeleteState(StateID id)
    {
        states.Remove(id);
    }

    // ִ��״̬ת���ķ���
    public void PerformTransition(Transition trans)
    {
        StateID id = currentState.GetOutputState(trans);
        FSMState fSMState = states[id];
        currentState.OnExit();
        currentState = fSMState;
        currentState.OnEnter();
    }
}