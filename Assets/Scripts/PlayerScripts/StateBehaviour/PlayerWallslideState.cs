using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallslideState : MonoBehaviour
{
    public PlayerState Tick(PlayerStateManager player)
    {
        player.rb.velocity = new Vector2(player.XMove * player.MovementSpeed, player.rb.velocity.y);

        if (Input.GetKey("z") && player.OnWall)
        {
            return player.WallgrabState;
        }
        else if (player.OnGround)
        {
            return player.IdleState;
        }
        else if (Input.GetKeyUp("c") && player.OnWall)
        {
            // Wall Jump
            return player.JumpState;
        }
        else
        {
            return player.FallState;
        }
    }

    public void Enter(PlayerStateManager player)
    {
        player.PlayerAnimator.SetBool("isWallSliding", true);
    }

    public void Exit(PlayerStateManager player)
    {
        player.PlayerAnimator.SetBool("isWallSliding", false);
    }

}
