using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]

/**
 * Class that manages the state-based player character movement
 */
public class PlayerStateManager : MonoBehaviour
{
    private IPlayerState currentState;

    // A set of all possible states the player may be in
    public PlayerIdleState IdleState = new PlayerIdleState();
    public PlayerJumpState JumpState = new PlayerJumpState();
    public PlayerMoveState MoveState = new PlayerMoveState();
    public PlayerWallgrabState WallgrabState = new PlayerWallgrabState();
    public PlayerFallState FallState = new PlayerFallState();
    public PlayerWallslideState WallslideState = new PlayerWallslideState();
    public PlayerDashState DashState = new PlayerDashState();

    // A check that ensures the dash cannot be called if it is not recharged
    public bool IsDashRecharged { get; set; } = true;
    public bool CanMove { get; set; } = true;

    public Rigidbody2D rb { get; private set; }
    private BoxCollider2D bc;

    // Movement input
    public float XMove { get; set; }
    public float YMove { get; set; }

    // Ground and wall collider checkers
    public bool OnGround { get; set; }
    public bool OnWall { get; set; }

    public Animator PlayerAnimator { get; private set; }

    public Transform Transform { get; private set; }

    public List<MovingPlatform> MovingPlatforms { get; set; } = new List<MovingPlatform>();

    public Vector2 MovingPlatformVelocity { get; set; } = new Vector2(0f, 0f);

    public LocalAchievementService AchievementService;

    [SerializeField] private LevelCompleteListener levelCompleteListener;

    /**
     * When enabled, set the current state to idle
     */
    private void OnEnable()
    {
        currentState = IdleState;
    }

    /**
     * On awake, initialise components
     */
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        PlayerAnimator = GetComponent<Animator>();
        Transform = GetComponent<Transform>();
    }

    /**
     * With each update, get the user inputs and change the current state
     */
    private void Update()
    {
        //Debug.Log(MovingPlatformVelocity);
        //Debug.Log(MovingPlatforms.Count);
        //Debug.Log(currentState);
        GetMoveInput();
        UpdateState();
        TimeReverseChecker();
    }

    /**
     * With each fixed update, update the player movement and check collider overlaps
     */
    private void FixedUpdate()
    {
        AddMovingPlatformVelocity();
        if (CanMove)
        {
            // Update movement depending on the current player state    
            currentState.FixedTick(this);
        }
        CheckTagOverlap();
    }

    /**
     * Update state, if the new state is different from the current state
     */
    private void UpdateState()
    {
        IPlayerState newState = currentState.Tick(this);
        if (!newState.Equals(currentState))
        {
            currentState.Exit(this);
            currentState = newState;
            currentState.Enter(this);
        }
    }

    /**
     * Get the x and y input from keyboard
     */
    private void GetMoveInput()
    {
        XMove = Input.GetAxisRaw("Horizontal");
        YMove = Input.GetAxisRaw("Vertical");
    }

    /**
     * Check if the ground trigger overlaps with the ground
     */
    public void OnGroundedChange(bool _onGround)
    {
        OnGround = _onGround;
        PlayerAnimator.SetBool("onGround", _onGround);
    }

    /**
     * Check if the wall trigger overlaps with the ground (walls are considered grounds)
     */
    public void OnWalledChange(bool _onWall)
    {
        OnWall = _onWall;
    }

    private void AddMovingPlatformVelocity()
    {
        Vector2 tempVelocity = Vector2.zero;
        for (int i = 0; i < MovingPlatforms.Count; i++)
        {
            tempVelocity += MovingPlatforms[i].GetVelocity();
        }
        MovingPlatformVelocity = tempVelocity;
    }

    public void LandedOnMovingPlatform(MovingPlatform moving)
    {
        if (!MovingPlatforms.Contains(moving))
        {
            MovingPlatforms.Add(moving);
        }
    }

    public void LeftMovingPlatform(MovingPlatform moving)
    {
        if (MovingPlatforms.Contains(moving))
        {
            MovingPlatforms.Remove(moving);
        }
    }

    /**
     * Send a message to the achievement service whenever the player uses the time reverse
     */
    private void TimeReverseChecker()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            AchievementService.BroadcastMessage("ReverseCheck", SendMessageOptions.DontRequireReceiver);
        }
    }

    /**
     * Check collider overlaps with other colliders
     */
    private void CheckTagOverlap()
    {
        ContactFilter2D contactFilter = new ContactFilter2D().NoFilter();
        List<Collider2D> collisions = new List<Collider2D>();
        bc.OverlapCollider(contactFilter, collisions);

        for (int i = 0; i < collisions.Count; i++)
        {
            string layerName = LayerMask.LayerToName(collisions[i].gameObject.layer);
            //Debug.Log(layerName);
            switch (layerName)
            {
                case "Deadly":
                    KillPlayer();
                    break;
                case "Spike":
                    if (!GameManager.UndoActive())
                    {
                        KillPlayer();
                    }
                    break;
                case "Enemy":
                    AchievementService.BroadcastMessage("FinishLevelJump", SendMessageOptions.DontRequireReceiver);
                    AchievementService.BroadcastMessage("TimeReverse", SendMessageOptions.DontRequireReceiver);
                    levelCompleteListener.OnLevelComplete();
                    break;
                case "TimeFruit":
                    GameManager.EnableUndo();
                    Destroy(collisions[i].gameObject);
                    IsDashRecharged = true;
                    break;
            }
        }
    }

    private void KillPlayer()
    {
        AudioManager.StopReverse();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.OnPlayerDead();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerJumper"))
        {
            if (!GameManager.UndoActive())
            {
                Boulder boulder = collision.gameObject.transform.parent.gameObject.GetComponent<Boulder>();
                boulder.DisableBoulder(true);
            }
            rb.velocity = new Vector2(0f, 10f);
            IsDashRecharged = true;
        }
    }
}
