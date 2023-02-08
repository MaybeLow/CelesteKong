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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        if (moveRight)
        {
            moveMultiplier = 1;
        }
        startingPos = transform.position;
        rb.AddForce(new Vector2(moveMultiplier, rb.velocity.y) * pushForce * 0.2f);
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
        if (rb.velocity.magnitude < 0.05f && startingPos != transform.position)
        {
            print(rb.velocity.magnitude);
            Destroy(this.gameObject);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundChecker.position, 0.5f, groundLayer);
    }
}
