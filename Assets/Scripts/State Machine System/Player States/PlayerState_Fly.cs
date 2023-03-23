using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/½ÇÉ«×´Ì¬/Fly", fileName = "PlayerState_Fly")]
public class PlayerState_Fly : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        player.Fly();
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (player.onGround)
        {
            player.StopRoll();
            player.StartRun();
            stateMachine.SwitchState(typeof(PlayerState_Run));
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
