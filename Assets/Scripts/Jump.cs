using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private const float jumpScale = 10f;
    [SerializeField] private const float gravityScale = 2f;
    [SerializeField] private const float fallingGravityScale = 3f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void JumpObject(Vector2 jumpDir, float jumpScale = jumpScale)
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
