using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Player wall slide state
 */
public class PlayerWallslideState : PlayerFlipper
{
    // The initial direction the player is facing when it starts wall sliding
    private float initialXMove;

    public override IPlayerState Tick(PlayerStateManager player)
    {
        // State transitions
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
        else if (player.OnWall)
        {
            return player.WallslideState;
        }
        else
        {
            return player.IdleState;
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

}
