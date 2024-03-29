using UnityEngine;

/**
 * Player fall state
 */
public class PlayerFallState : PlayerWallJumper
{
    private float fallMovementSpeed = 10f;

    public override IPlayerState Tick(PlayerStateManager player)
    {
        // Update the rotation of the player
        UpdateFlip(player);

        // State transitions
        if (Input.GetKey("z") && player.OnWall)
        {
            return player.WallgrabState;
        }
        else if (Input.GetKeyDown("c") && player.OnWall)
        {
            WallJump(player);
            return player.JumpState;
        }
        else if (player.OnGround)
        {
            return player.IdleState;
        }
        else if (Input.GetKeyDown("x") && player.IsDashRecharged)
        {
            return player.DashState;
        }
        else if (Mathf.Abs(player.XMove) > 0 && player.OnWall)
        {
            return player.WallslideState;
        }
        else if (player.rb.velocity.y > 0f)
        {
            return player.JumpState;
        }
        else
        {
            return player.FallState;
        }
    }

    public override void FixedTick(PlayerStateManager player)
    {
        player.rb.velocity = new Vector2(player.XMove * fallMovementSpeed, player.rb.velocity.y);
    }

    /**
     * On enter, set the animator in isFalling state
     */
    public override void Enter(PlayerStateManager player)
    {
        player.PlayerAnimator.SetBool("isFalling", true);
    }

    /**
     * On exit, set the isFalling state to false
     */
    public override void Exit(PlayerStateManager player)
    {
        player.PlayerAnimator.SetBool("isFalling", false);
    }
}
