using UnityEngine;

/**
 * Player wall grab state
 */
public class PlayerWallgrabState : IPlayerState
{
    private float climbSpeed = 7f;
    private float climbFinishJumpHeigth = 8f;

    // The initial direction the player is facing when a wall is grabbed
    private float initialXMove;

    public IPlayerState Tick(PlayerStateManager player)
    {
        // State transitions
        if (Input.GetKeyDown("c") && initialXMove != player.XMove && player.XMove != 0f)
        {
            return player.JumpState;
        }
        else if (Input.GetKey("z") && player.OnWall)
        {
            return player.WallgrabState;
        }
        else if (Mathf.Abs(player.rb.velocity.y) > 0 && !player.OnWall)
        {
            player.rb.velocity = new Vector2(player.rb.velocity.x, climbFinishJumpHeigth);
            return player.JumpState;
        }
        else
        {
            return player.IdleState;
        }
    }

    public void FixedTick(PlayerStateManager player)
    {
        player.rb.velocity = new Vector2(player.rb.velocity.x, player.YMove * climbSpeed);
    }

    /**
     * On enter, set the animator in isWallClimbing state and initialise the initial direction
     */
    public void Enter(PlayerStateManager player)
    {
        player.IsDashRecharged = true;
        initialXMove = player.XMove;
        player.PlayerAnimator.SetBool("isWallClimbing", true);
    }

    /**
     * On exit, set the isWallClimbing state to false and change vertical velocity
     */
    public void Exit(PlayerStateManager player)
    {
        // Change the vertical velocity to make the player do a small jump when climbed a wall
        player.rb.velocity = new Vector2(player.rb.velocity.x, player.rb.velocity.y * 0.7f);
        player.PlayerAnimator.SetBool("isWallClimbing", false);
    }
}
