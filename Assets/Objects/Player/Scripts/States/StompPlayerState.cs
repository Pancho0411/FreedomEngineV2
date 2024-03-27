public class StompPlayerState : PlayerState
{
    public override void Enter(Player player)
    {
        player.attacking = true; //should be redundant but just keeping it to make sure
        player.ChangeBounds(0);
        player.PlayAudio(player.audios.boostStart, 1f);
        player.particles.stompAura.Play();
    }

    public override void Step(Player player, float deltaTime)
    {
        // Apply gravity to make the player fall faster
        player.HandleGravity(deltaTime * player.stats.stompMultiplier);
        player.velocity.x = 0;

        // Check if the player is grounded
        if (player.grounded)
        {
            player.PlayAudio(player.audios.stompLand, 1f);

            if (player.input.action)
            {
                player.state.ChangeState<JumpPlayerState>();
            }
            else {
                // If the player hits the ground while stomping, exit to the WalkPlayerState
                player.state.ChangeState<WalkPlayerState>();
            }
        }
    }

    public override void Exit(Player player)
    {
        player.particles.stompAura.Stop();
    }
}
