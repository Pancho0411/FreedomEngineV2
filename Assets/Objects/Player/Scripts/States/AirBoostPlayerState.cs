using UnityEngine;
using System.Collections;

public class AirBoostPlayerState : PlayerState
{
    float camSpeed;
    public float camSpeedInterpolationDuration = 0.5f;

    public override void Enter(Player player)
    {
        player.attacking = true;
        player.ChangeBounds(0);
        player.PlayAudio(player.audios.boostStart, 1f);
        player.particles.boostAura.Play();
        camSpeed = player.camera.maxSpeed;

        if (player.input.boostActionDown && player.stats.boostMeter > 0)
        {
            // Apply boost speed based on direction
            player.velocity.x = player.stats.boostSpeed * player.direction;
        }
    }

    public override void Step(Player player, float deltaTime)
    {
        player.UpdateDirection(player.input.horizontal);
        player.HandleSlopeFactor(deltaTime);
        player.HandleAcceleration(deltaTime);
        player.HandleFriction(deltaTime);
        player.HandleGravity(deltaTime);
        player.HandleFall();
        player.camera.maxSpeed = 200;

        // Check if the player is grounded
        if (player.grounded && player.input.boostAction)
        {
            player.state.ChangeState<BoostState>();
        }
        else if(player.grounded && !player.input.boostAction)
        {
            player.state.ChangeState<WalkPlayerState>();
        }

        // Clamp velocity to a maximum value
        player.velocity.x = Mathf.Clamp(player.velocity.x, -player.stats.maxSpeed, player.stats.maxSpeed);
    }

    public override void Exit(Player player)
    {
        player.particles.boostAura.Stop();
        player.StartCoroutine(InterpolateCameraSpeed(player.camera, camSpeed, camSpeedInterpolationDuration));
    }

    private IEnumerator InterpolateCameraSpeed(PlayerCamera camera, float targetSpeed, float duration)
    {
        float elapsedTime = 0;
        float startSpeed = camera.maxSpeed;

        // Interpolate the camera speed over the specified duration
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            camera.maxSpeed = Mathf.Lerp(startSpeed, targetSpeed, elapsedTime / duration);
            yield return null;
        }

        // Ensure the camera speed is set to the target speed at the end
        camera.maxSpeed = targetSpeed;
    }
}
