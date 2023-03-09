using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

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

    // Temporary
    //[SerializeField] private BoxCollider2D gc;
    //[SerializeField] public BoxCollider2D wc;
    //[SerializeField] private LayerMask groundMask;

    // Movement input
    public float XMove { get; set; }
    public float YMove { get; set; }

    // Ground and wall collider checkers
    public bool OnGround { get; set; }
    public bool OnWall { get; set; }

    public Animator PlayerAnimator { get; private set; }

    public Transform Transform { get; private set; }

    [SerializeField] private Camera playerCamera;

    public List<MovingPlatform> MovingPlatforms { get; set; } = new List<MovingPlatform>();

    public Vector2 MovingPlatformVelocity { get; set; } = new Vector2(0f, 0f);

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
        Debug.Log(MovingPlatforms.Count);
        //Debug.Log(currentState);
        GetMoveInput();
        UpdateState();
    }

    /**
     * With each fixed update, update the player movement and check collider overlaps
     */
    private void FixedUpdate()
    {
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

    public void LandedOnMovingPlatform(MovingPlatform moving)
    {
        if (!MovingPlatforms.Contains(moving))
        {
            MovingPlatforms.Add(moving);
            MovingPlatformVelocity += moving.GetVelocity();
        }
    }

    public void LeftMovingPlatform(MovingPlatform moving)
    {
        if (MovingPlatforms.Contains(moving))
        {
            MovingPlatforms.Remove(moving);
            MovingPlatformVelocity -= moving.GetVelocity();
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
            switch (collisions[i].tag)
            {
                case "Boulder":
                    print("DEAD");
                    //Destroy(this.gameObject);
                    //Game Over
                    break;
                case "Spike":
                    print("DEAD");
                    //Destroy(this.gameObject);
                    //Game Over
                    break;
            }
        }
    }
}
