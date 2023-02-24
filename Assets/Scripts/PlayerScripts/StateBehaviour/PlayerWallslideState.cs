using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallslideState : PlayerState
{
    private float initialXMove;
    public PlayerState Tick(PlayerStateManager player)
    {
        WallSlide(player);
        if (Input.GetKeyDown("c")) {
            return player.JumpState;
        }
        else if (Input.GetKey("z"))
        {
            return player.WallgrabState;
        }
        else if (player.XMove != initialXMove)
        {
            return player.FallState;
        }
        else if (player.OnGround)
        {
            return player.IdleState;
        }
        else
        {
            return player.WallslideState;
        }
    }

    public void Enter(PlayerStateManager player)
    {
        initialXMove = player.XMove;
        player.PlayerAnimator.SetBool("isWallSliding", true);
    }

    public void Exit(PlayerStateManager player)
    {
        player.PlayerAnimator.SetBool("isWallSliding", false);
    }

    private void WallSlide(PlayerStateManager player)
    {
        player.rb.velocity = new Vector2(player.rb.velocity.x, Mathf.Clamp(player.rb.velocity.y, -player.WallSlideSpeed, float.MaxValue));
    }

}
