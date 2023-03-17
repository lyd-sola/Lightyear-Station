using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/½ÇÉ«×´Ì¬/Jump", fileName = "PlayerState_Jump")]
public class PlayerState_Jump : PlayerState
{
    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(player.ySpeed < 0)
            stateMachine.SwitchState(typeof(PlayerState_Fall));
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
