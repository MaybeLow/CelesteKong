using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * An abstract class that rotates the player if the horizontal movement changes
 */
public abstract class PlayerFlipper : IPlayerState
{
    public abstract IPlayerState Tick(PlayerStateManager player);
    public abstract void Enter(PlayerStateManager player);
    public abstract void Exit(PlayerStateManager player);

    /**
     * Check which direction the player is facing and rotate if needed
     */
    protected void UpdateFlip(PlayerStateManager player)
    {
        if (player.XMove < -0.1f && player.transform.localScale.x >= 0
            || player.XMove > 0.1f && player.transform.localScale.x < -0)
        {
            FlipPlayer(player);
        }
    }

    /**
     * Flip the player
     */
    protected void FlipPlayer(PlayerStateManager player)
    {
        Vector3 localScale = player.transform.localScale;
        localScale.x *= -1;
        player.transform.localScale = localScale;
    }
}
