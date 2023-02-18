using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (pm.onWall && !pm.onGround && !pm.onWallGrab && pm.xMove != 0)
        {
            pm.isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
        }
        else
        {
            pm.isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (pm.onWall && (pm.isWallSliding || pm.onWallGrab))
        {
            pm.isWallJumping = false;
            wallJumpDir = -(transform.localScale.x / Mathf.Abs(transform.localScale.x));
            wallJumpCounter = wallJumpTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        // state, command
        else
        {
            wallJumpCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown("c") && wallJumpCounter > 0f && pm.onWall)
        {
            pm.isWallJumping = true;
            pm.isWallSliding = false;
            pm.onWall = false;

            rb.AddForce(new Vector2(wallJumpDir, 1) * wallJumpForce);

            wallJumpCounter = 0;

            Invoke(nameof(StopWallJumping), wallJumpDuration);
        }
    }

    private void StopWallJumping()
    {
        pm.isWallJumping = false;
    }
}
