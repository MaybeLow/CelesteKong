using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float speed = 5;
    private float jump = 7;
    private float xMove;
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private Transform currentFloor;

    [SerializeField] private Transform groundChecker;
    [SerializeField] private Transform wallChecker;
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
        UpdateMovement();
        UpdateFlip();
        CheckOverlap();
    }

    private void UpdateMovement()
    {
        xMove = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && (IsGrounded() || IsWalled()))
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.3f);
        }
    }

    private void UpdateFlip()
    {
        if ((xMove < -0.1f && transform.localScale.x > 0.1f)
            || (xMove > 0.1f && transform.localScale.x < -0.1f))
        {
            FlipPlayer();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(xMove * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundChecker.position, 0.2f, groundLayer);
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallChecker.position, 0.2f, groundLayer);
    }

    private void FlipPlayer()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void CheckOverlap()
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
