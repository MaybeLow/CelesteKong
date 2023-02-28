using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Player fall state
 */
public class PlayerFallState : PlayerFlipper
{
    public override IPlayerState Tick(PlayerStateManager player)
    {
        // Update the rotation of the player
        UpdateFlip(player);

        // State transitions
        if (Input.GetKey("z") && player.OnWall)
        {
            return player.WallgrabState;
        }
        else if (player.OnGround)
        {
            return player.IdleState;
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

    /**
     * On enter, set the animator in isFalling state
     */
    public override void Enter(PlayerStateManager player)
    {
        player.PlayerAnimator.SetBool("isFalling", true);
    }

    /**
     * On exit, set the isFalling state to false
     */
    public override void Exit(PlayerStateManager player)
    {
        player.PlayerAnimator.SetBool("isFalling", false);
    }
}
