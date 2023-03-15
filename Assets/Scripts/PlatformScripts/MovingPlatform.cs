using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour, IPoolableObject
{
    [SerializeField] private Vector2 velocity;

    private MovingPlatformSpawner spawner;
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

    public void AssignSpawner(MovingPlatformSpawner spawner)
    {
        this.spawner = spawner;
    }

    public Vector2 GetVelocity()
    {
        return rb.velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlatformDestroyer")) {
            PoolObject();
        }
    }

    public void PoolObject()
    {
        gameObject.SetActive(false);
        spawner.AddOnPool(gameObject);
    }
}
