using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerWallJumper : PlayerFlipper
{
    private float wallJumpTime = 0.2f;
    private float jumpHeight = 10f;

    public abstract override IPlayerState Tick(PlayerStateManager player);
    public abstract override void FixedTick(PlayerStateManager player);
    public abstract override void Enter(PlayerStateManager player);
    public abstract override void Exit(PlayerStateManager player);


    protected void WallJump(PlayerStateManager player)
    {
        player.StartCoroutine(DontMove(player));
        FlipPlayer(player);
        float dashDirection = player.Transform.localScale.x / Mathf.Abs(player.Transform.localScale.x);
        player.rb.velocity = new Vector2(dashDirection * jumpHeight, jumpHeight * 1.2f);
    }

    private IEnumerator DontMove(PlayerStateManager player)
    {
        player.CanMove = false;
        yield return new WaitForSeconds(wallJumpTime);
        player.CanMove = true;
    }
}
