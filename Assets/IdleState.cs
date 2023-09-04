using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerState
{
    public override void Enter(Player player)
    {
        
    }

    public override void Step(Player player, float deltaTime)
    {
        if (player.grounded)
        {
            if (player.input.actionDown)
            {
                player.HandleJump();
            }
            else if (Mathf.Abs(player.velocity.x) < player.stats.minSpeedToUnroll)
            {
                player.state.ChangeState<WalkPlayerState>();
            }
            else if(player.input.actionUp)
            {
                if (Mathf.Abs(player.velocity.x) < player.stats.minSpeedToRoll)
                {
                    player.state.ChangeState<LookUpPlayerState>();
                }
            }
            else if(player.input.down)
            {
                player.state.ChangeState<CrouchPlayerState>();
            }
        }
    }

    public override void Exit(Player player)
    {
        
    }
}
