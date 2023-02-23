using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerState
{
    public PlayerState Tick(PlayerStateManager player);
    public void Enter(PlayerStateManager player);
    public void Exit(PlayerStateManager player);
}
