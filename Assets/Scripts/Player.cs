using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float speed = 5;
    private float climbSpeed = 5;
    private float jump = 12;
    private float xMove;
    private float yMove;
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private Transform currentFloor;
    private bool onWall = false;

    [SerializeField] private Transform groundChecker;
    [SerializeField] private Transform wallGrabChecker;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Camera playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        xMove = Input.GetAxisRaw("Horizontal");
        yMove = Input.GetAxisRaw("Vertical");

        UpdateInput();
        UpdateFlip();
        CheckTagOverlap();
    }

    private void UpdateInput()
    {
        if (Input.GetButtonDown("Jump")) {
            UpdateJump();
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.3f);
        }

        if (Input.GetKey("z") && IsWallGrabbed())
        {
            onWall = true;
        }
        else
        {
            onWall = false;
        }
    }

    private void UpdateJump()
    {
        if (IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
        } else if (IsWallGrabbed())
        {
            FlipPlayer();
            // jump top right
            rb.velocity = new Vector2(rb.velocity.x, jump); 
        }
    }

    private void UpdateFlip()
    {
        if (!onWall && 
            ((xMove < -0.1f && transform.localScale.x > 0.1f)
            || (xMove > 0.1f && transform.localScale.x < -0.1f)))
        {
            FlipPlayer();
        }
    }

    private void FixedUpdate()
    {
        if (!onWall)
        {
            rb.velocity = new Vector2(xMove * speed, rb.velocity.y);
        } else
        {
            rb.velocity = new Vector2(rb.velocity.x, yMove * climbSpeed);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundChecker.position, 0.2f, groundLayer);
    }

    private bool IsWallGrabbed()
    {
        return Physics2D.OverlapCircle(wallGrabChecker.position, 0.2f, groundLayer);
    }

    private void FlipPlayer()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void CheckTagOverlap()
    {
        ContactFilter2D contactFilter = new ContactFilter2D().NoFilter();
        List<Collider2D> collisions = new List<Collider2D>();
        bc.OverlapCollider(contactFilter, collisions);

        for (int i = 0; i < collisions.Count; i++)
        {
            switch (collisions[i].tag)
            {
                case "Boulder":
                    print("DEAD");
                    Destroy(this.gameObject);
                    //Game Over
                    break;

                case "Floor":
                    if (collisions[i].transform != currentFloor)
                    {
                        print(collisions[i].transform);
                        currentFloor = collisions[i].transform;
                        playerCamera.transform.position = new Vector3(currentFloor.position.x, currentFloor.position.y, playerCamera.transform.position.z);
                    }
                    break;
            }
        }
    }
}
