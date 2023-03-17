using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : ScriptableObject, IState
{
    // stateName should be the same as animation name
    [SerializeField] string stateName;
    int stateHash;

    // crossFade
    [SerializeField, Range(0f, 1f)] float transitionDuration = 0.1f;

    protected Animator animator;
    protected PlayerStateMachine stateMachine;
    protected Player player;
    protected PlayerInput input;

    // Animation time
    private float stateStartTime;
    protected float StateDuration => Time.time - stateStartTime;

    protected bool isAnimationFinished => StateDuration >= animator.GetCurrentAnimatorStateInfo(0).length;

    public void Initialize(Animator animator, PlayerStateMachine stateMachine, Player player, PlayerInput input)
    {
        this.stateMachine = stateMachine;
        this.animator = animator;
        this.player = player;
        this.input = input;

        stateHash = Animator.StringToHash(stateName);
    }

    public virtual void Enter()
    {
        animator.CrossFade(stateHash, transitionDuration);
        stateStartTime = Time.time;
    }

    public virtual void Exit()
    {
        
    }

    public virtual void LogicUpdate()
    {
        
    }

    public virtual void PhysicUpdate()
    {
        
    }
}
