using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerState Tick(PlayerStateManager player)
    {
        if (Input.GetKeyDown("c") && player.OnGround)
        {
            return player.JumpState;
        }
        else if ((Mathf.Abs(player.XMove) > 0) && player.OnGround)
        {
            return player.MoveState;
        }
        else
        {
            return player.IdleState;
        }
    }

    public void Enter(PlayerStateManager player)
    {
        player.PlayerAnimator.SetBool("isRunning", false);
        player.PlayerAnimator.SetBool("isJumping", false);
        player.PlayerAnimator.SetBool("isDashing", false);
        player.PlayerAnimator.SetBool("isFalling", false);
    }

    public void Exit(PlayerStateManager player)
    {
        //Destroy(this);
    }
}
