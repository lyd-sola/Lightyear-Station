using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/��ɫ״̬/Fly", fileName = "PlayerState_Fly")]
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
            player.StartRun();
            player.StopRoll();
            stateMachine.SwitchState(typeof(PlayerState_Run));
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
