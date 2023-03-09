using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Vector2 velocity;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        MovePlatform();
    }

    public void MovePlatform()
    {
        rb.velocity = velocity;
    }

    public Vector2 GetVelocity()
    {
        return rb.velocity;
    }
}
