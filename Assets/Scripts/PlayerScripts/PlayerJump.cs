using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerManager))]

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

    private void FixedUpdate()
    {
        UpdateGravity();
    }

    private void JumpInput()
    {
        if (!pm.OnWallGrab && !pm.IsWallJumping)
        {
            // Perform a high jump
            if (Input.GetKeyDown("c") && pm.OnGround && !pm.IsDashing)
            {
                Jump(Vector2.up, jumpScale);
            }
            // Perform a low jump if the jump button is released early
            if (Input.GetKeyUp("c") && rb.velocity.y > 0 && !pm.IsDashing)
            {
                Jump(Vector2.up, jumpScale * 0.3f);
            }
        }
    }

    private void Jump(Vector2 jumpDir, float jumpScale)
    {
        rb.velocity = jumpDir * jumpScale;
    }

    private void UpdateGravity()
    {
        if (rb.velocity.y > 0 && !pm.OnGround)
        {
            rb.gravityScale = gravityScale;
            PlayJumpAnimations();
        }
        else if (rb.velocity.y < 0 && !pm.OnGround)
        {
            rb.gravityScale = fallingGravityScale;
            PlayFallAnimations();
        } else
        {
            ResetAnimations();
        }
    }

    private void PlayJumpAnimations()
    {
        pm.playerAnimator.SetBool("isJumping", true);
        pm.playerAnimator.SetBool("isFalling", false);
    }

    private void PlayFallAnimations()
    {
        pm.playerAnimator.SetBool("isJumping", false);
        pm.playerAnimator.SetBool("isFalling", true);
    }

    private void ResetAnimations()
    {
        pm.playerAnimator.SetBool("isJumping", false);
        pm.playerAnimator.SetBool("isFalling", false);
    }
}
