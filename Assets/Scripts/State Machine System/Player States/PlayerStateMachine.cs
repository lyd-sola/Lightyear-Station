using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ʵ�ֽ�ɫ״̬����StateMachine�̳���MonoBehaviour
public class PlayerStateMachine : StateMachine
{
    [SerializeField] PlayerState[] states;  // SerializeFieldʹ˽�ж��������Inspector����ʾ

    Animator animator;

    protected override void Update()
    {
        currentState.LogicUpdate();
    }

    protected override void FixedUpdate()
    {
        currentState.PhysicUpdate();
    }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        stateTable = new Dictionary<System.Type, IState>(states.Length);

        // Do player states initialization here
        foreach (var state in states)
        {
            state.Initialize(animator, this, GetComponent<Player>(), GetComponent<PlayerInput>());
            stateTable.Add(state.GetType(), state);
        }

        SwitchOn(stateTable[typeof(PlayerState_Run)]);

    }

    private void Start()
    {
        
    }
}
