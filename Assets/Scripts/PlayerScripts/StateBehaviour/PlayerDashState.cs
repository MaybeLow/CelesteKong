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
    private float enterXMove;
    private float enterYMove;
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
        float dashDirection = player.Transform.localScale.x / Mathf.Abs(player.Transform.localScale.x);

        if (enterXMove == 0f && enterYMove != 0f)
        {
            player.rb.velocity = new Vector2(0, enterYMove * dashScale * 0.5f);
            player.Transform.rotation = Quaternion.Euler(player.Transform.rotation.x, player.Transform.rotation.y, 90f * dashDirection * enterYMove);
        }
        else if (enterXMove != 0f && enterYMove != 0f)
        {
            player.rb.velocity = new Vector2(dashDirection * dashScale * 0.7f, enterYMove * dashScale * 0.7f);
            player.Transform.rotation = Quaternion.Euler(player.Transform.rotation.x, player.Transform.rotation.y, 45f * dashDirection * enterYMove);
        }
        else
        {
            player.rb.velocity = new Vector2(dashDirection * dashScale, 0f);
        }
    }

    /**
     * On enter, set the animator in isDashing state, initialise the initial time, 
     * and set gravity to 0 temporarily
     */
    public void Enter(PlayerStateManager player)
    {
        enterXMove = player.XMove;
        enterYMove = player.YMove;
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
