using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour, IEntity, IPoolableObject
{
    private BoulderCommandController controller;
    [SerializeField] private bool undoActive;

    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    protected CircleCollider2D circleCollider;

    [SerializeField] private Transform groundChecker;
    [SerializeField] private LayerMask groundLayer;
    private Vector2 moveDirection = Vector2.left;
    private bool onGround = false;

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
        if (onGround)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ObjectPooler"))
        {
            DisableBoulder();
        }
    }

    public void OnGroundedChange(bool _onGround)
    {
        if (_onGround == true && onGround == false)
        {
            moveDirection *= -1f;
        }
        onGround = _onGround;
    }

    public void PoolObject()
    {
        moveDirection = Vector2.left;
        controller.ResetCommandList();
        gameObject.SetActive(false);
        spawner.AddOnPool(gameObject);
    }
}
