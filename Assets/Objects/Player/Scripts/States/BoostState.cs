using UnityEngine;
using System.Collections;

public class BoostState : PlayerState
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

        if (player.velocity.x == 0)
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
        if (player.grounded & player.attacking)
        {
            // Check if the boost action is pressed and there is boost meter remaining
            if (player.input.boostAction && player.stats.boostMeter > 0)
            {
                // Boost when action button is held down and there's boost meter remaining
                player.velocity.x += player.input.horizontal * player.stats.boostSpeed * deltaTime;
                player.stats.boostMeter -= player.stats.boostMeterConsumptionRate * deltaTime;

                // Clamp the boost meter to prevent it from going below zero
                player.stats.boostMeter = Mathf.Max(0, player.stats.boostMeter);
            }
            else
            {
                player.attacking = false;
                // If boost button is released or boost meter is empty, switch to another state
                player.state.ChangeState<WalkPlayerState>();
            }

            // Check for other actions like jumping or rolling
            if (player.input.actionDown)
            {
                player.HandleJump();
            }
            else if (player.input.down)
            {
                if (Mathf.Abs(player.velocity.x) > player.stats.minSpeedToRoll)
                {
                    player.state.ChangeState<RollPlayerState>();
                    player.PlayAudio(player.audios.spin, 0.5f);
                }
            }
        }
        else
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
