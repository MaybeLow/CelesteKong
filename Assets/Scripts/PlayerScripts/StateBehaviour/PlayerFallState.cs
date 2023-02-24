using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerState
{
    public PlayerState Tick(PlayerStateManager player)
    {
        UpdateFlip(player);

        if (Input.GetKey("z") && player.OnWall)
        {
            return player.WallgrabState;
        }
        else if (player.OnGround)
        {
            return player.IdleState;
        }
        else if (Input.GetKeyUp("c") && player.OnWall)
        {
            // Wall Jump
            return player.JumpState;
        }
        else if (Mathf.Abs(player.XMove) > 0 && player.OnWall)
        {
            return player.WallslideState;
        }
        else
        {
            return player.FallState;
        }
    }

    public void Enter(PlayerStateManager player)
    {
        player.PlayerAnimator.SetBool("isFalling", true);
    }

    public void Exit(PlayerStateManager player)
    {
        player.PlayerAnimator.SetBool("isFalling", false);
    }

    private void UpdateFlip(PlayerStateManager player)
    {
        if (player.XMove < -0.1f && player.transform.localScale.x >= 0
            || player.XMove > 0.1f && player.transform.localScale.x < -0)
        {
            FlipPlayer(player);
        }
    }

    private void FlipPlayer(PlayerStateManager player)
    {
        Vector3 localScale = player.transform.localScale;
        localScale.x *= -1;
        player.transform.localScale = localScale;
    }
}
