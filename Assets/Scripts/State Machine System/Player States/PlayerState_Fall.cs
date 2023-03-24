using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/½ÇÉ«×´Ì¬/Fall", fileName = "PlayerState_Fall")]
public class PlayerState_Fall : PlayerState
{
    bool fastFall;
    public override void Enter()
    {
        base.Enter();
        fastFall = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // jump
        if (!fastFall && input.jumpPressed && player.jumpTimes > 0 && Upgrades.instance.GetUpgradeStat("ErDuanTiao") != 0)
        {
            input.jumpTime = -1f; // reset jump state
            --player.jumpTimes;
            stateMachine.SwitchState(typeof(PlayerState_Jump));
        }
        
        // landing
        if (player.onGround)
        {
            stateMachine.SwitchState(typeof(PlayerState_Run));
        }

        // Roll
        if (input.rollPressed && !fastFall && Upgrades.instance.GetUpgradeStat("JiSuXiaZhui") == 1)
        {
            fastFall = true;
            player.FallFast();
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
