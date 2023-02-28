using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallslideState : PlayerFlipper
{
    private float initialXMove;

    public override PlayerState Tick(PlayerStateManager player)
    {
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

    public override void Enter(PlayerStateManager player)
    {
        initialXMove = player.XMove;
        player.PlayerAnimator.SetBool("isWallSliding", true);
    }

    public override void Exit(PlayerStateManager player)
    {
        player.PlayerAnimator.SetBool("isWallSliding", false);
    }

}
