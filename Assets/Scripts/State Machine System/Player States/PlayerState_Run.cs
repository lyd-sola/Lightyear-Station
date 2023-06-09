using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/��ɫ״̬/Run", fileName = "PlayerState_Run")]
public class PlayerState_Run : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        // Reset jumpTimes
        player.jumpTimes = player.playerData.jumpTimes;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // jump
        if (input.jumpPressed && player.jumpTimes > 0)
        {
            input.jumpTime = -1f; // reset jump state
            //--player.jumpTimes;
            stateMachine.SwitchState(typeof(PlayerState_Jump));
        }

        // Roll
        if(input.rollPressed)
        {
            stateMachine.SwitchState(typeof(PlayerState_Roll));
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
