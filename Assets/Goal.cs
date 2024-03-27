using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Goal : PlayerState
{
    public override void Enter(Player player)
    {
        player.velocity = Vector2.zero;
        player.rotation = 90;
    }

    public override void Step(Player player, float deltaTime)
    {
        //player.state.ChangeState<WalkPlayerState>();
    }

    public override void Exit(Player player)
    {
        
    }
}