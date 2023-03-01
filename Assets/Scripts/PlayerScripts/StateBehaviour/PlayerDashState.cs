using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

/**
 * Player dash state
 */
public class PlayerDashState : IPlayerState
{
    private float dashScale = 20f;
    private float dashTime = 0.4f;
    private float dashCooldownTime = 2f;
    private float dashEnterTime;
    private float savedGravity;

    public IPlayerState Tick(PlayerStateManager player)
    {
        // Transit to a different state when the dash timer runs out
        if (Time.time - dashEnterTime >= dashTime) 
        {
            return player.IdleState;
        } 
        // Switch state when the player touches a wall.
        else if (player.OnWall)
        {
            return player.WallslideState;
        }
        else
        {
            return player.DashState;
        }
    }

    public void FixedTick(PlayerStateManager player)
    {
        if (player.XMove == 0f && player.YMove != 0f)
        {
            player.rb.velocity = new Vector2(0, player.YMove * dashScale * 0.5f);
        }
        else if (player.XMove != 0f && player.YMove != 0f)
        {
            float dashDirection = player.Transform.localScale.x / Mathf.Abs(player.Transform.localScale.x);
            player.rb.velocity = new Vector2(dashDirection * dashScale * 0.7f, player.YMove * dashScale * 0.7f);
        }
        else
        {
            float dashDirection = player.Transform.localScale.x / Mathf.Abs(player.Transform.localScale.x);
            player.rb.velocity = new Vector2(dashDirection * dashScale, 0f);
        }
    }

    /**
     * On enter, set the animator in isDashing state, initialise the initial time, 
     * and set gravity to 0 temporarily
     */
    public void Enter(PlayerStateManager player)
    {
        dashEnterTime = Time.time;
        player.PlayerAnimator.SetBool("isDashing", true);

        savedGravity = player.rb.gravityScale;
        player.rb.gravityScale = 0;
    }

    /**
     * On exit, set the isDashing state to false, reset rotation and gravity, 
     * and start a dash cooldown coroutine
     */
    public void Exit(PlayerStateManager player)
    {
        player.PlayerAnimator.SetBool("isDashing", false);

        player.transform.rotation = Quaternion.identity;
        player.rb.gravityScale = savedGravity;

        player.StartCoroutine(DashCooldown(player));
    }

    private IEnumerator DashCooldown(PlayerStateManager player)
    {
        player.IsDashRecharged = false;
        yield return new WaitForSeconds(dashCooldownTime);
        player.IsDashRecharged = true;
    }
}
