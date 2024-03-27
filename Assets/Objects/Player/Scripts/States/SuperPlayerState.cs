using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperPlayerState : PlayerState
{
    public override void Enter(Player player)
    {
        player.disableInput = true;
        player.velocity = Vector3.zero;
        poweringUp();
        player.disableInput = false;
    }

    public override void Step(Player player, float deltaTime)
    {
        //player.state.ChangeState<WalkPlayerState>();
    }

    public override void Exit(Player player)
    {

    }

    public IEnumerator poweringUp()
    {
        yield return new WaitForSeconds(3f);
    }
}
