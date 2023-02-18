using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void FixedUpdate()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        // Update horizontal movement
        if (!pm.onWallGrab && !pm.isWallJumping)
        {
            rb.velocity = new Vector2(pm.xMove * speed, rb.velocity.y);
            UpdateFlip();
        }
    }

    private void UpdateFlip()
    {
        if (pm.xMove < -0.1f && transform.localScale.x >= 0
            || pm.xMove > 0.1f && transform.localScale.x < -0)
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
