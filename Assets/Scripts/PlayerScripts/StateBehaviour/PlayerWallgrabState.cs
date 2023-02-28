using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallgrabState : PlayerState
{
    public PlayerState Tick(PlayerStateManager player)
    {
        if (Input.GetKeyDown("c"))
        {
            return player.JumpState;
        }
        else if (Input.GetKey("z") && player.OnWall)
        {
            return player.WallgrabState;
        }
        else if (Mathf.Abs(player.rb.velocity.y) > 0 && !player.OnWall)
        {
            return player.FallState;
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
        player.rb.velocity = new Vector2(player.rb.velocity.x, player.rb.velocity.y * 0.7f);
        player.PlayerAnimator.SetBool("isWallClimbing", false);
    }
}
