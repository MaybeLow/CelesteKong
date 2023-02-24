using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallgrabState : PlayerState
{
    public PlayerState Tick(PlayerStateManager player)
    {
        WallClimb(player);
        // Perform a low jump if the jump button is released early
        if (Input.GetKeyDown("c"))
        {
            return player.JumpState;
        }
        else if (Input.GetKey("z") && player.OnWall)
        {
            return player.WallgrabState;
        }
        else if (player.rb.velocity.y > 0 && !player.OnWall)
        {
            return player.JumpState;
        }
        else
        {
            return player.IdleState;
        }
    }

    public void Enter(PlayerStateManager player)
    {
        player.PlayerAnimator.SetBool("isWallClimbing", true);
    }

    public void Exit(PlayerStateManager player)
    {
        player.PlayerAnimator.SetBool("isWallClimbing", false);
    }

    private void WallClimb(PlayerStateManager player)
    {
        // Update vertical movement
        player.rb.velocity = new Vector2(player.rb.velocity.x, player.YMove * player.ClimbSpeed);
    }
}
