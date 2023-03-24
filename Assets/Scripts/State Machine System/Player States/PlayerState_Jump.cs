using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/½ÇÉ«×´Ì¬/Jump", fileName = "PlayerState_Jump")]
public class PlayerState_Jump : PlayerState
{
    bool fastFall;
    public override void Enter()
    {
        base.Enter();
        fastFall = false;
        player.Jump();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // fall
        if(player.ySpeed < 0)
            stateMachine.SwitchState(typeof(PlayerState_Fall));

        // jump
        if (!fastFall && input.jumpPressed && player.jumpTimes > 0 && Upgrades.instance.GetUpgradeStat("ErDuanTiao") != 0)
        {
            input.jumpTime = -1f; // reset jump state
            --player.jumpTimes;
            stateMachine.SwitchState(typeof(PlayerState_Jump));
        }

        // Roll
        if (input.rollPressed && !fastFall && Upgrades.instance.GetUpgradeStat("JiSuXiaZhui") != 0)
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
