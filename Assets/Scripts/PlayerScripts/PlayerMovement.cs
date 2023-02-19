using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerManager))]

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerManager pm;

    private float speed = 5;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pm = GetComponent<PlayerManager>();
    }

    private void Update()
    {
        UpdateFlip();
    }

    private void FixedUpdate()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        // Update horizontal movement
        if (!pm.OnWallGrab && !pm.IsWallJumping && !pm.IsDashing)
        {
            rb.velocity = new Vector2(pm.XMove * speed, rb.velocity.y);
            PlayRunningAnimation();
        }
    }

    private void PlayRunningAnimation()
    {
        // Play running animation
        if ((rb.velocity.x > 0 || rb.velocity.x < 0) && pm.OnGround)
        {
            pm.playerAnimator.SetBool("isRunning", true);
        }
        else
        {
            pm.playerAnimator.SetBool("isRunning", false);
        }
    }

    private void UpdateFlip()
    {
        if (!pm.OnWallGrab && !pm.IsDashing &&
            (pm.XMove < -0.1f && transform.localScale.x >= 0
            || pm.XMove > 0.1f && transform.localScale.x < -0))
        {
            FlipPlayer();
        }
    }

    private void FlipPlayer()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
