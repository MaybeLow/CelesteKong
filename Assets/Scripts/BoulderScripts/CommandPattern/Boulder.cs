using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour, IEntity, IPoolableObject
{
    private BoulderCommandController controller;
    private Vector2 moveDirection;
    [SerializeField] private bool undoActive;

    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    protected CircleCollider2D circleCollider;

    [SerializeField] private Transform groundChecker;
    [SerializeField] private LayerMask groundLayer;

    Rigidbody2D IEntity.rb => rb;

    private Spawner spawner;

    private float timeWhenDisabled;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<BoulderCommandController>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        moveDirection = Vector2.right;
        undoActive = false;
    }

    private void Update()
    {
        CheckDisableTime();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
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
        } else if (undoActive)
        {
            UpdateUndo();
        } else
        {
            controller.ExecuteCommand(new BoulderEmptyCommand(this, Time.timeSinceLevelLoad));
        }
    }

    private void UpdateMovement() {
        if (IsGrounded())
        {
            controller.ExecuteCommand(new BoulderMoveCommand(this, Time.timeSinceLevelLoad, moveDirection));
        }
        else
        {
            controller.ExecuteCommand(new BoulderFallCommand(this, Time.timeSinceLevelLoad));
        }
    }

    public void AssignSpawner(Spawner spawner)
    {
        this.spawner = spawner;
    }

    public void DisableBoulder()
    {
        timeWhenDisabled = Time.time;
        controller.ExecuteCommand(new BoulderDisableCommand(this, Time.timeSinceLevelLoad, sr, circleCollider));
    }

    private void CheckDisableTime()
    {
        if (sr.enabled == false && undoActive == false)
        {
            if (Time.time - timeWhenDisabled > 10.0f)
            {
                controller.ExecuteCommand(new BoulderEnableCommand(this, Time.timeSinceLevelLoad, sr, circleCollider));
                PoolObject();
            }
        }
        
    }

    private void UpdateUndo()
    {
        controller.UndoCommand();
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundChecker.position, 0.1f, groundLayer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ObjectPooler"))
        {
            DisableBoulder();
        }
    }

    public void PoolObject()
    {
        gameObject.SetActive(false);
        spawner.AddOnPool(gameObject);
    }
}
