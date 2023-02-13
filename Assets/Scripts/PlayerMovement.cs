using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Jump jp;

    private float jumpScale = 1;

    private float speed = 5;

    private float climbSpeed = 5;
    private bool onWallGrab = false;
    private bool onGround = false;
    private bool onWall = false;

    private bool isWallSliding = false;
    private float wallSlideSpeed = 2.4f;

    private bool isWallJumping;
    private float wallJumpDir;
    // Wall jump cooldown
    private float wallJumpTime = 0.17f;
    private float wallJumpCounter;
    private float wallJumpDuration = 0.3f;
    private Vector2 wallJumpScale = new Vector2(8f, 12f);

    private float xMove;
    private float yMove;

    [SerializeField] private Transform groundChecker;
    [SerializeField] private Transform wallGrabChecker;
    [SerializeField] private LayerMask groundLayer;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        jp = GetComponent<Jump>();
    }

    // Update is called once per frame
    private void Update()
    {
        GetMoveInput();
        GroundJump();
        WallGrub();
        WallSlide();
        WallJump();
    }

    private void FixedUpdate()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        // Update horizontal movement
        if (!onWallGrab && !isWallJumping)
        {
            rb.velocity = new Vector2(xMove * speed, rb.velocity.y);
            UpdateFlip();
        }
        // Update vertical movement
        else if (onWallGrab && !isWallJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, yMove * climbSpeed);
        }
    }

    private void GetMoveInput()
    {
        xMove = Input.GetAxisRaw("Horizontal");
        yMove = Input.GetAxisRaw("Vertical");
    }

    private void GroundJump()
    {
        if (!onWallGrab && !isWallJumping)
        {
            // Perform a high jump
            if (Input.GetButtonDown("Jump") && onGround)
            {
                jp.JumpObject(new Vector2(rb.velocity.x, jumpScale));
            }
            // Perform a low jump if the jump button is released early
            if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
            {
                jp.JumpObject(new Vector2(rb.velocity.x, jumpScale * 0.3f));
            }
        }
    }

    private void WallGrub()
    {
        if (Input.GetKey("z") && onWall)
        {
            onWallGrab = true;
        }
        else
        {
            onWallGrab = false;
        }
    }

    private void UpdateFlip()
    {
        if (!isWallSliding &&
            ((xMove < -0.1f && transform.localScale.x >= 0)
            || (xMove > 0.1f && transform.localScale.x < -0)))
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

    private void WallSlide()
    {
        if (onWall && !onGround && !onWallGrab && xMove != 0) {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
        } 
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (onWall && (isWallSliding || onWallGrab))
        {
            print("1  --  " + onWall);
            isWallJumping = false;
            wallJumpDir = -(transform.localScale.x / Mathf.Abs(transform.localScale.x));
            wallJumpCounter = wallJumpTime;

            CancelInvoke(nameof(StopWallJumping));
        } 
        // state, command
        else
        {
            wallJumpCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpCounter > 0f && onWall)
        {
            print("a");
            isWallJumping = true;
            isWallSliding = false;
            onWall = false;

            FlipPlayer();

            jp.JumpObject(new Vector2(wallJumpDir * wallJumpScale.x, wallJumpScale.y), 1);

            wallJumpCounter = 0;

            Invoke(nameof(StopWallJumping), wallJumpDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    public void OnGroundedChange(bool _onGround)
    {
        onGround= _onGround;
    }

    public void OnWalledChange(bool _onWall)
    {
        print("2  ++  " + _onWall);
        onWall = _onWall;
    }   
}
