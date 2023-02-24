using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerState Tick(PlayerStateManager player)
    {
        UpdateFlip(player);
        // Perform a low jump if the jump button is released early
        if (Input.GetKeyUp("c") && player.rb.velocity.y > 0)
        {
            Jump(player, Vector2.up, player.JumpScale * 0.3f);
        }

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
        else if (Mathf.Abs(player.XMove) > 0 && player.OnWall)
        {
            return player.WallslideState;
        }
        else
        {
            return player.JumpState;
        }
    }

    public void Enter(PlayerStateManager player)
    {
        Jump(player, Vector2.up, player.JumpScale);

        player.PlayerAnimator.SetBool("isJumping", true);
    }

    public void Exit(PlayerStateManager player)
    {
        player.PlayerAnimator.SetBool("isJumping", false);
    }

    private void Jump(PlayerStateManager player, Vector2 jumpDir, float jumpScale)
    {
        player.rb.velocity = jumpDir * jumpScale;
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
