using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 playerDir;
    private bool onGround = false;

    [SerializeField] private Transform player;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            if (player != null)
            {
                playerDir = player.position - transform.position;
                rb.velocity = new Vector2(playerDir.x * 0.5f, 0);
            }
        }

        if (!IsGrounded())
        {
            onGround = false;
        }
    }

    private void CheckIfStopped()
    {
        if (rb.velocity.magnitude < 0.02f)
        {
            Destroy(this.gameObject);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundChecker.position, 0.5f, groundLayer);
    }
}
