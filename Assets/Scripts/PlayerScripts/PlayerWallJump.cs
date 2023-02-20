using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerManager))]

public class PlayerWallJump : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerManager pm;

    private float wallJumpForce = 270f;
    private float wallSlideSpeed = 2.4f;
    private float wallJumpDir;
    private float wallJumpTime = 0.17f;
    private float wallJumpCounter;
    private float wallJumpDuration = 0.3f;
    private Vector2 wallJumpScale = new Vector2(8f, 12f);

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pm = GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        WallJump();
    }

    private void FixedUpdate()
    {
        WallSlide();
    }

    private void WallSlide()
    {
        if (pm.OnWall && !pm.OnGround && !pm.OnWallGrab && pm.XMove != 0)
        {
            pm.IsWallSliding = true;
            pm.PlayerAnimator.SetBool("isWallSliding", true);
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
        }
        else
        {
            pm.IsWallSliding = false;
            pm.PlayerAnimator.SetBool("isWallSliding", false);
        }
    }

    private void WallJump()
    {
        if (pm.OnWall && (pm.IsWallSliding || pm.OnWallGrab))
        {
            pm.IsWallJumping = false;
            wallJumpDir = -(transform.localScale.x / Mathf.Abs(transform.localScale.x));
            wallJumpCounter = wallJumpTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        // state, command
        else
        {
            wallJumpCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown("c") && wallJumpCounter > 0f && pm.OnWall)
        {
            pm.IsWallJumping = true;
            pm.IsWallSliding = false;
            pm.OnWall = false;

            rb.AddForce(new Vector2(wallJumpDir, 1) * wallJumpForce);

            wallJumpCounter = 0;

            Invoke(nameof(StopWallJumping), wallJumpDuration);
        }
    }

    private void StopWallJumping()
    {
        pm.IsWallJumping = false;
    }
}
