using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Player move state
 */
public class PlayerMoveState : PlayerFlipper
{
    private float movementSpeed = 10f;

    public override IPlayerState Tick(PlayerStateManager player)
    {
        // Update the rotation of the player
        UpdateFlip(player);

        // State transitions
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

    public override void FixedTick(PlayerStateManager player)
    {
        player.rb.velocity = new Vector2(player.XMove * movementSpeed, player.rb.velocity.y);
    }

    /**
     * On enter, set the animator in isRunning state
     */
    public override void Enter(PlayerStateManager player)
    {
        player.PlayerAnimator.SetBool("isRunning", true);
    }

    /**
     * On exit, set the isRunning state to false
     */
    public override void Exit(PlayerStateManager player)
    {
        player.PlayerAnimator.SetBool("isRunning", false);
    }
}
