using System.Diagnostics;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class JumpPlayerState : PlayerState
{
    public PlayerParticles particles;
    public AudioSource fireEffectSource;
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
            else if (player.input.actionDown && particles.fireShield.isPlaying)
            {
                particles.fireShield.Stop();
                fireEffectSource.Play();
                player.velocity.x = player.stats.boostSpeed * player.direction;
                particles.fireAura.Play();
            }
        }
		else
		{
			//handle fire aura when landing
			if(particles.fireAura.isPlaying) { 
				particles.fireAura.Stop(); 
				particles.fireShield.Play();
            }

			player.state.ChangeState<WalkPlayerState>();
		}
	}

    public override void Exit(Player player)
    {
        player.rotation = player.originalRotation;
		player.attacking = false;
    }
}