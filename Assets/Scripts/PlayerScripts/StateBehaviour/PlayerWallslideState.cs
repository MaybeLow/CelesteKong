using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

/**
 * Player wall slide state
 */
public class PlayerWallslideState : PlayerWallJumper
{
    private float wallSlideSpeed = 2f;

    // The initial direction the player is facing when it starts wall sliding
    private float initialXMove;

    public override IPlayerState Tick(PlayerStateManager player)
    {
        // State transitions
        if (Input.GetKeyDown("c"))
        {
            WallJump(player);
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
        else if (!player.OnWall)
        {
            return player.JumpState;
        }
        else
        {
            return player.WallslideState;
        }
    }

    public override void FixedTick(PlayerStateManager player)
    {
        player.rb.velocity = new Vector2(player.rb.velocity.x, Mathf.Clamp(player.rb.velocity.y, -wallSlideSpeed, float.MaxValue));
    }

    /**
     * On enter, set the animator in isWallSliding state and initialise the initial direction
     */
    public override void Enter(PlayerStateManager player)
    {
        player.IsDashRecharged = true;
        initialXMove = player.XMove;
        player.PlayerAnimator.SetBool("isWallSliding", true);
    }

    /**
     * On exit, set the isWallSliding state to false
     */
    public override void Exit(PlayerStateManager player)
    {
        player.PlayerAnimator.SetBool("isWallSliding", false);
    }
}
