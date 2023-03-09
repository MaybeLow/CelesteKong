using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Vector2 velocity;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlatformDestroyer")) {
            gameObject.SetActive(false);
            MovingPlatformSpawner.AddOnPool(gameObject);
        }
    }
}
