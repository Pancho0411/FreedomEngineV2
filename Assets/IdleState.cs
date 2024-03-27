using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerState
{
    public override void Enter(Player player)
    {
        player.rotation = 0f;
    }

    public override void Step(Player player, float deltaTime)
    {
        if (player.input.actionDown)
        {
            player.HandleJump();
        }
        else if (player.input.down)
        {
            player.state.ChangeState<CrouchPlayerState>();
        }
        else if (player.input.up)
        {
            player.state.ChangeState<LookUpPlayerState>();
        }
        else if(player.input.right || player.input.left)
        {
            player.state.ChangeState<WalkPlayerState>();
        }
    }

    public override void Exit(Player player)
    {
        player.rotation = player.originalRotation;
    }
}
