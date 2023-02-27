using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerFlipper
{
    public override PlayerState Tick(PlayerStateManager player)
    {
        UpdateFlip(player);
        
        if (Input.GetKeyDown("c") && player.OnGround)
        {
            return player.JumpState;
        }
        else if (Mathf.Abs(player.XMove) == 0)
        {
            return player.IdleState;
        }
        else if (Input.GetKey("z") && player.OnWall)
        {
            return player.WallgrabState;
        }
        else if (Input.GetKeyDown("x") && player.IsDashRecharged)
        {
            return player.DashState;
        }
        else if (player.rb.velocity.y < 0 && !player.OnGround)
        {
            return player.FallState;
        }
        else
        {
            return player.MoveState;
        }
    }

    public override void Enter(PlayerStateManager player)
    {
        player.PlayerAnimator.SetBool("isRunning", true);
    }

    public override void Exit(PlayerStateManager player)
    {
        player.PlayerAnimator.SetBool("isRunning", false);
    }
}
