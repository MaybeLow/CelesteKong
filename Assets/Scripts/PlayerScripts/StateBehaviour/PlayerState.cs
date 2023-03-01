using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Interface class that manages the 3 basic state methods
 */
public interface IPlayerState
{
    // Tick is called each frame for the state the player is currently in
    public IPlayerState Tick(PlayerStateManager player);
    // Fixed tick to update character movement
    public void FixedTick(PlayerStateManager player);
    // Enter is called whenever the player enters the state
    public void Enter(PlayerStateManager player);
    // Exit is called whenever the player exits the state
    public void Exit(PlayerStateManager player);
}
