using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// StateMachine:
// ��������״̬�࣬������״̬����
public class StateMachine : MonoBehaviour
{
    [SerializeField] protected IState currentState;

    protected Dictionary<System.Type, IState> stateTable;

    protected virtual void Update()
    {
        currentState.LogicUpdate();
    }

    protected virtual void FixedUpdate()
    {
        currentState.PhysicUpdate();
    }

    protected void SwitchOn(IState newState)
    {
        currentState = newState;
        currentState.Enter();
    }

    public void SwitchState(IState newState)
    {
        currentState.Exit();
        SwitchOn(newState);
    }
    public void SwitchState(System.Type newStateType)
    {
        SwitchState(stateTable[newStateType]);
    }
}
