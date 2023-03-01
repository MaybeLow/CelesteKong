using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/**
 * Player idle state
 */
public class PlayerIdleState : IPlayerState
{
    public IPlayerState Tick(PlayerStateManager player)
    {
        // State transitions
        if (Input.GetKeyDown("c") && player.OnGround)
        {
            return player.JumpState;
        }
        else if ((Mathf.Abs(player.XMove) > 0) && player.OnGround)
        {
            return player.MoveState;
        }
        else if (player.rb.velocity.y < 0 && !player.OnGround)
        {
            return player.FallState;
        }
        else if (Input.GetKeyDown("x") && player.IsDashRecharged)
        {
            return player.DashState;
        }
        else if (Input.GetKey("z") && player.OnWall)
        {
            return player.WallgrabState;
        }
        else
        {
            return player.IdleState;
        }
    }

    public void FixedTick(PlayerStateManager player)
    {
        player.rb.velocity = new Vector2(0f, 0f);
    }

    /**
     * Idle state does not have an animator state, thus the method is empty
     */
    public void Enter(PlayerStateManager player)
    {
        
    }

    /**
     * Idle state does not have an animator state, thus the method is empty
     */
    public void Exit(PlayerStateManager player)
    {
        
    }
}
