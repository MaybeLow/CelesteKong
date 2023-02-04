using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool onGround = false;
    private int moveMultiplier = -1;
    private float pushForce = 150f;
    private Vector3 startingPos;

    [SerializeField] private Transform groundChecker;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool moveRight;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (moveRight)
        {
            moveMultiplier = 1;
        }
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
        CheckIfStopped();
    }

    private void UpdateMovement() {
        if (IsGrounded() && !onGround)
        {
            onGround = true;
            rb.AddForce(new Vector2(moveMultiplier, 0) * pushForce);
        }

        if (!IsGrounded())
        {
            onGround = false;
        }
    }

    private void CheckIfStopped()
    {
        if (rb.velocity.magnitude < 0.02f && startingPos != transform.position)
        {
            Destroy(this.gameObject);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundChecker.position, 0.5f, groundLayer);
    }
}
