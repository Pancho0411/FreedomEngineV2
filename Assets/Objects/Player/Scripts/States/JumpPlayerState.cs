using System.Diagnostics;

public class JumpPlayerState : PlayerState
{
	public override void Enter(Player player)
	{
		player.attacking = true;
		player.ChangeBounds(1);
        player.rotation = 0f;
    }

	public override void Step(Player player, float deltaTime)
	{
		player.UpdateDirection(player.input.horizontal);
		player.HandleAcceleration(deltaTime);
		player.HandleGravity(deltaTime);

		if (!player.grounded && player.attacking)
		{
			if (player.input.actionUp && player.velocity.y > player.stats.minJumpHeight)
			{
				player.velocity.y = player.stats.minJumpHeight;
			}
			else if (player.input.stompAction)
			{
				player.state.ChangeState<StompPlayerState>();
			}
			else if (player.input.boostAction && !player.input.action)
			{
				player.state.ChangeState<AirBoostPlayerState>();
			}
		}
		else
		{
			player.state.ChangeState<WalkPlayerState>();
		}
	}

    public override void Exit(Player player)
    {
        player.rotation = player.originalRotation;
    }
}