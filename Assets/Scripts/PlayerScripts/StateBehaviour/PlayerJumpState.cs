using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Player jump state
 */
public class PlayerJumpState : PlayerWallJumper
{
    private float jumpMovementSpeed = 10f;

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
        if (player.rb.velocity.y < 0f && !player.OnGround)
        {
            return player.FallState;
        }
        else if (Input.GetKeyDown("c") && player.OnWall)
        {
            WallJump(player);
            return player.JumpState;
        }
        else if (Input.GetKey("z") && player.OnWall)
        {
            return player.WallgrabState;
        }
        else if (Input.GetKeyDown("x") && player.IsDashRecharged)
        {
            return player.DashState;
        }
        else if (Mathf.Abs(player.XMove) > 0f && player.OnWall && player.rb.velocity.y < 0f)
        {
            return player.WallslideState;
        }
        else if (player.rb.velocity.y <= 0f && player.OnGround)
        {
            return player.IdleState;
        }
        else
        {
            return player.JumpState;
        }
    }

    public override void FixedTick(PlayerStateManager player)
    {
        player.rb.velocity = new Vector2(player.XMove * jumpMovementSpeed, player.rb.velocity.y);
    }

    /**
     * On enter, set the animator in isJumping state and jump
     */
    public override void Enter(PlayerStateManager player)
    {
        if (player.OnGround)
        {
            Jump(player, Vector2.up, JumpScale);
        }

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
