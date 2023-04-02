using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour, IPEntity
{
    private PlatformCommandController controller;

    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    protected BoxCollider2D bc;

    private Vector2 moveDirection;

    Rigidbody2D IPEntity.rb => rb;
    BoxCollider2D IPEntity.bc => bc;
    SpriteRenderer IPEntity.sr => sr;

    [SerializeField] private Vector2 velocity;

    private Spawner spawner;

    private float timeWhenDisabled;

    private void Awake()
    {
        bc = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<PlatformCommandController>();
    }

    private void Start()
    {
        moveDirection = new Vector2(spawner.GetDirection(), 0);
    }

    private void CheckDisableTime()
    {
        if (sr.enabled == false && !GameManager.UndoActive())
        {
            if (Time.time - timeWhenDisabled > 10.0f)
            {
                controller.ExecuteCommand(new PlatformEnableCommand(this, Time.timeSinceLevelLoad, sr, bc, velocity));
                PoolObject();
            }
        }
    }

    public void PoolObject()
    {
        controller.ResetCommandList();
        gameObject.SetActive(false);
        spawner.AddOnPool(gameObject);
    }

    private void Update()
    {
        CheckDisableTime();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        OnReturnToStart();
        if (sr.enabled)
        {
            if (GameManager.UndoActive())
            {
                UpdateUndo();
            }
            else
            {
                UpdateMovement();
            }
        }
        else if (GameManager.UndoActive())
        {
            UpdateUndo();
        }
        else
        {
            ExecuteEmpty();
        }
    }

    private void ExecuteEmpty()
    {
        controller.ExecuteCommand(new PlatformEmptyCommand(this, Time.timeSinceLevelLoad));
    }

    private void UpdateMovement()
    {
        controller.ExecuteCommand(new PlatformMoveCommand(this, Time.timeSinceLevelLoad, moveDirection));
    }

    public void AssignSpawner(Spawner spawner)
    {
        this.spawner = spawner;
    }

    public void DisablePlatform()
    {
        timeWhenDisabled = Time.time;
        controller.ExecuteCommand(new PlatformDisableCommand(this, Time.timeSinceLevelLoad, sr, bc, velocity));
    }


    private void UpdateUndo()
    {
        controller.UndoCommand();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("ObjectPooler") || collision.CompareTag("PlatformDestroyer")) && !GameManager.UndoActive())
        {
            DisablePlatform();
        }
    }

    public void OnReturnToStart()
    {
        if (controller.IsEmpty() && GameManager.UndoActive())
        {
            PoolObject();
        }
    }

    public Vector2 GetVelocity()
    {
        return rb.velocity;
    }
}
