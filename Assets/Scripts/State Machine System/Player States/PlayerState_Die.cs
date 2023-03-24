using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/½ÇÉ«×´Ì¬/Die", fileName = "PlayerState_Die")]
public class PlayerState_Die : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        player.NoGravity();
    }
    
    // will switch status by animation event
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
