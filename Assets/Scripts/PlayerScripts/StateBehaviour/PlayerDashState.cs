using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerDashState : PlayerState
{
    private float dashEnterTime;
    private float savedGravity;

    public PlayerState Tick(PlayerStateManager player)
    {
        if (Time.time - dashEnterTime >= player.DashTime) 
        {
            return player.IdleState;
        }
        else
        {
            return player.DashState;
        }
    }

    public void Enter(PlayerStateManager player)
    {
        dashEnterTime = Time.time;
        player.PlayerAnimator.SetBool("isDashing", true);

        savedGravity = player.rb.gravityScale;
        player.rb.gravityScale = 0;
    }

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
        yield return new WaitForSeconds(player.DashCooldownTime);
        player.IsDashRecharged = true;
    }
}
