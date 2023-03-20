using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour, IPEntity, IPoolableObject
{
    private PlatformCommandController controller;
    [SerializeField] private bool undoActive;

    protected Rigidbody2D rb;
    protected SpriteRenderer sr;

    private Vector2 moveDirection = Vector2.right;

    Rigidbody2D IPEntity.rb => rb;

    [SerializeField] private Vector2 velocity;

    private Spawner spawner;

    private float timeWhenDisabled;

    private List<PlatformCommand> commandBuffer = new List<PlatformCommand>();

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<PlatformCommandController>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        undoActive = false;
    }

    //private void OnEnable()
    //{
    //  MovePlatform();
    //}

    //public void MovePlatform()
    //{
    //  rb.velocity = velocity;
    //}

    private void Update()
    {
        CheckDisableTime();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (controller.IsEmpty())
        {
            rb.velocity = Vector2.zero;
        }

        if (sr.enabled)
        {
            if (undoActive)
            {
                UpdateUndo();
            }
            else
            {
                UpdateMovement();
            }
        }
        else if (undoActive)
        {
            UpdateUndo();
        }
        else
        {
            controller.ExecuteCommand(new PlatformEmptyCommand(this, Time.timeSinceLevelLoad));
        }
    }

    private void UpdateMovement()
    {
        controller.ExecuteCommand(new PlatformMoveCommand(this, Time.timeSinceLevelLoad, moveDirection));
    }

    public void AssignSpawner(Spawner spawner)
    {
        this.spawner = spawner;
    }

    private void UpdateUndo()
    {
        if (!controller.IsEmpty())
        {
            controller.UndoCommand();
        } else
        {
            commandBuffer.Add(new PlatformEmptyCommand(this, Time.timeSinceLevelLoad));
        }
        
    }

    public void DisablePlatform()
    {
        rb.velocity = Vector2.zero;
        timeWhenDisabled = Time.time;
        controller.ExecuteCommand(new PlatformDisableCommand(this, Time.timeSinceLevelLoad, sr));
    }

    private void CheckDisableTime()
    {
        if (sr.enabled == false && undoActive == false)
        {
            if (Time.time - timeWhenDisabled > 10.0f)
            {
                controller.ExecuteCommand(new PlatformEnableCommand(this, Time.timeSinceLevelLoad, sr));
                PoolObject();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ObjectPooler") || collision.CompareTag("PlatformDestroyer"))
        {
            DisablePlatform();
        }
    }

    public Vector2 GetVelocity()
    {
      return rb.velocity;
    }

    public void PoolObject()
    {
        gameObject.SetActive(false);
        controller.ResetCommandList();
        spawner.AddOnPool(gameObject);
    }
}
