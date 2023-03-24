using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/½ÇÉ«×´Ì¬/Roll", fileName = "PlayerState_Roll")]
public class PlayerState_Roll : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        player.Roll();
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // StopRoll
        if(!input.rollPressed)
        {
            //player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            player.StopRoll();
            stateMachine.SwitchState(typeof(PlayerState_Run));
        }

        // SuperBall
        if(input.jumpPressed && Upgrades.instance.GetUpgradeStat("ChaoJiQiu") != 0)
        {
            stateMachine.SwitchState(typeof(PlayerState_SuperBall));
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
