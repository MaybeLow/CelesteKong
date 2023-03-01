using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

/**
 * Player wall slide state
 */
public class PlayerWallslideState : PlayerFlipper
{
    // The initial direction the player is facing when it starts wall sliding
    private float initialXMove;
    private float wallJumpTime = 0.2f;

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
        else
        {
            return player.WallslideState;
        }
    }

    /**
     * On enter, set the animator in isWallSliding state and initialise the initial direction
     */
    public override void Enter(PlayerStateManager player)
    {
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

    private void WallJump(PlayerStateManager player)
    {
        player.StartCoroutine(DontMove(player));
        FlipPlayer(player);
        float dashDirection = player.transform.localScale.x / Mathf.Abs(player.transform.localScale.x);
        player.rb.AddForce(new Vector2(dashDirection * 500f, 700f));
    }

    private IEnumerator DontMove(PlayerStateManager player)
    {
        player.CanMove = false;
        yield return new WaitForSeconds(wallJumpTime);
        player.CanMove = true;
    }
}
