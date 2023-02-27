using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerFlipper
{
    public override PlayerState Tick(PlayerStateManager player)
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
        else if (Input.GetKeyDown("x") && player.IsDashRecharged)
        {
            return player.DashState;
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

    public override void Enter(PlayerStateManager player)
    {
        player.PlayerAnimator.SetBool("isFalling", true);
    }

    public override void Exit(PlayerStateManager player)
    {
        player.PlayerAnimator.SetBool("isFalling", false);
    }
}
