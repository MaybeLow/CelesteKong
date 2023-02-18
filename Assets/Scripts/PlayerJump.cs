using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerManager pm;

    [SerializeField] private float jumpScale = 10f;
    [SerializeField] private float gravityScale = 2f;
    [SerializeField] private float fallingGravityScale = 3f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pm = GetComponent<PlayerManager>();
    }

    private void Update()
    {
        JumpInput();
    }

    private void JumpInput()
    {
        if (!pm.onWallGrab && !pm.isWallJumping)
        {
            // Perform a high jump
            if (Input.GetKeyDown("c") && pm.onGround && !pm.isDashing)
            {
                Jump(Vector2.up, jumpScale);
            }
            // Perform a low jump if the jump button is released early
            if (Input.GetKeyUp("c") && rb.velocity.y > 0 && !pm.isDashing)
            {
                Jump(Vector2.up, jumpScale * 0.3f);
            }
        }
    }

    private void Jump(Vector2 jumpDir, float jumpScale)
    {
        rb.velocity = jumpDir * jumpScale;

        if (rb.velocity.y >= 0)
        {
            rb.gravityScale = gravityScale;
        }
        else if (rb.velocity.y < 0)
        {
            rb.gravityScale = fallingGravityScale;
        }
    }
}
