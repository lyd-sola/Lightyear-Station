using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/½ÇÉ«×´Ì¬/SuperBall", fileName = "PlayerState_SuperBall")]
public class PlayerState_SuperBall : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        //player.Roll();
        player.Jump();
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // landing
        if (player.onGround)
        {
            stateMachine.SwitchState(typeof(PlayerState_Run));
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
