using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerFlipper : PlayerState
{
    public abstract PlayerState Tick(PlayerStateManager player);
    public abstract void Enter(PlayerStateManager player);
    public abstract void Exit(PlayerStateManager player);

    protected void UpdateFlip(PlayerStateManager player)
    {
        if (player.XMove < -0.1f && player.transform.localScale.x >= 0
            || player.XMove > 0.1f && player.transform.localScale.x < -0)
        {
            FlipPlayer(player);
        }
    }

    protected void FlipPlayer(PlayerStateManager player)
    {
        Vector3 localScale = player.transform.localScale;
        localScale.x *= -1;
        player.transform.localScale = localScale;
    }
}
