using UnityEngine;

public class Boulder : MonoBehaviour, IEntity, IPoolableObject
{
    private BoulderCommandController controller;

    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    protected CircleCollider2D circleCollider;

    [SerializeField] private Transform groundChecker;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector2 startingDirection = Vector2.left;
    private Vector2 moveDirection;
    private bool onGround = false;

    Rigidbody2D IEntity.rb => rb;

    private Spawner spawner;

    private float timeWhenDisabled;

    private AudioSource breakSound;
    AudioSource IEntity.breakSound => breakSound;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<BoulderCommandController>();
        breakSound = GetComponent<AudioSource>();
        breakSound.volume = DataManager.SoundVolume;
    }

    private void Start()
    {
        moveDirection = startingDirection;
    }

    private void Update()
    {
        CheckDisableTime();
    }

    // Execute a command for the boulder depending on the state of the game
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

    // Execute an empty command to occupy the queue.
    // An empty command is required to make the object wait for some time.
    // For example, when it is being hidden, but not disabled.
    private void ExecuteEmpty()
    {
        controller.ExecuteCommand(new BoulderEmptyCommand(this, Time.timeSinceLevelLoad));
    }

    // Execute a movement command
    private void UpdateMovement()
    {
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

    // Execute a disable command.
    // When disabled, the boulder will wait for some time in case a rewind button is pressed.
    public void DisableBoulder(bool triggeredByPlayer)
    {
        timeWhenDisabled = Time.time;
        controller.ExecuteCommand(new BoulderDisableCommand(this, Time.timeSinceLevelLoad, sr, circleCollider, triggeredByPlayer, this));
    }

    // Update the time to poo the object if it's disabled for too long.
    private void CheckDisableTime()
    {
        if (sr.enabled == false && !GameManager.UndoActive())
        {
            if (Time.time - timeWhenDisabled > GameManager.GetRewindTime())
            {
                controller.ExecuteCommand(new BoulderEnableCommand(this, Time.timeSinceLevelLoad, sr, circleCollider));
                PoolObject();
            }
        }
    }

    // Execute the undo command
    private void UpdateUndo()
    {
        controller.UndoCommand();
    }

    // Pool the object if it's hit a boundary
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ObjectPooler"))
        {
            DisableBoulder(false);
        }
    }

    // A boulder changes its direction when it hits the ground
    public void OnGroundedChange(bool _onGround)
    {
        if (_onGround == true && onGround == false && !GameManager.UndoActive())
        {
            controller.ExecuteCommand(new BoulderDirectionCommand(this, Time.timeSinceLevelLoad, this));
        }
        onGround = _onGround;
    }

    public void ChangeBoulderDirection()
    {
        moveDirection *= -1f;
    }

    public void PoolObject()
    {
        moveDirection = startingDirection;
        controller.ResetCommandList();
        gameObject.SetActive(false);
        spawner.AddOnPool(gameObject);
    }

    // Empty the command list when the object is pooled
    public void OnReturnToStart()
    {
        if (controller.IsEmpty() && GameManager.UndoActive())
        {
            PoolObject();
        }
    }
}

