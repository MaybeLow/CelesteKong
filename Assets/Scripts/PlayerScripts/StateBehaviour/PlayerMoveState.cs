using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    public PlayerState Tick(PlayerStateManager player)
    {
        UpdateFlip(player);
        player.rb.velocity = new Vector2(player.XMove * player.MovementSpeed, player.rb.velocity.y);
        if (Mathf.Abs(player.XMove) > 0)
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
        player.PlayerAnimator.SetBool("isRunning", true);
        player.PlayerAnimator.SetBool("isJumping", false);
        player.PlayerAnimator.SetBool("isDashing", false);
        player.PlayerAnimator.SetBool("isFalling", false);
    }

    public void Exit(PlayerStateManager player)
    {
        //lol
    }

    private void UpdateFlip(PlayerStateManager player)
    {
        if (player.XMove < -0.1f && player.transform.localScale.x >= 0
            || player.XMove > 0.1f && player.transform.localScale.x < -0)
        {
            FlipPlayer(player);
        }
    }

    private void FlipPlayer(PlayerStateManager player)
    {
        Vector3 localScale = player.transform.localScale;
        localScale.x *= -1;
        player.transform.localScale = localScale;
    }
}
