using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Player jump state
 */
public class PlayerJumpState : PlayerFlipper
{
    // The power of jump
    private float JumpScale = 10f;
    public override IPlayerState Tick(PlayerStateManager player)
    {
        // Flip the direction the player is facing
        UpdateFlip(player);

        // Perform a low jump if the jump button is released early
        if (Input.GetKeyUp("c") && player.rb.velocity.y > 0)
        {
            Jump(player, Vector2.up, JumpScale * 0.3f);
        }

        // State transitions
        if (player.rb.velocity.y <= 0)
        {
            return player.FallState;
        }
        else if (Input.GetKey("z") && player.OnWall)
        {
            return player.WallgrabState;
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
            return player.JumpState;
        }
    }

    /**
     * On enter, set the animator in isJumping state and jump
     */
    public override void Enter(PlayerStateManager player)
    {
        Jump(player, Vector2.up, JumpScale);

        player.PlayerAnimator.SetBool("isJumping", true);
    }

    /**
     * On exit, set the isJumping state to false
     */
    public override void Exit(PlayerStateManager player)
    {
        player.PlayerAnimator.SetBool("isJumping", false);
    }

    /**
     * Perform jump
     */
    private void Jump(PlayerStateManager player, Vector2 jumpDir, float jumpScale)
    {
        player.rb.velocity = jumpDir * jumpScale;
    }
}
