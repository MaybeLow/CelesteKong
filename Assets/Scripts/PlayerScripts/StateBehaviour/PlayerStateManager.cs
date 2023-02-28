using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class PlayerStateManager : MonoBehaviour
{
    private PlayerState currentState;

    public PlayerIdleState IdleState = new PlayerIdleState();
    public PlayerJumpState JumpState = new PlayerJumpState();
    public PlayerMoveState MoveState = new PlayerMoveState();
    public PlayerWallgrabState WallgrabState = new PlayerWallgrabState();
    public PlayerFallState FallState = new PlayerFallState();
    public PlayerWallslideState WallslideState = new PlayerWallslideState();
    public PlayerDashState DashState = new PlayerDashState();


    public float MovementSpeed { get; set; } = 10f;
    public float JumpMovementSpeed { get; set; } = 10f;
    public float FallMovementSpeed { get; set; } = 10f;
    public float ClimbSpeed { get; set; } = 10f;
    public float WallSlideSpeed { get; set; } = 2f;
    public float DashScale { get; set; } = 20f;

    public bool IsDashRecharged { get; set; } = true;

    public Rigidbody2D rb { get; private set; }
    private BoxCollider2D bc;

    public float XMove { get; set; }
    public float YMove { get; set; }

    public bool OnGround { get; set; }
    public bool OnWall { get; set; }

    public Animator PlayerAnimator { get; private set; }

    [SerializeField] private Camera playerCamera;


    private void OnEnable()
    {
        currentState = IdleState;
    }
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        PlayerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        print(currentState);
        GetMoveInput();
        UpdateState();
    }

    private void FixedUpdate()
    {
        UpdateMovement();
        CheckTagOverlap();
    }

    private void UpdateState()
    {
        PlayerState newState = currentState.Tick(this);
        if (!newState.Equals(currentState))
        {
            currentState.Exit(this);
            currentState = newState;
            currentState.Enter(this);
        }
    }

    private void UpdateMovement()
    {
        if (currentState == IdleState)
        {
            rb.velocity = new Vector2(0f, 0f);
        }
        else if (currentState == MoveState)
        {
            rb.velocity = new Vector2(XMove * MovementSpeed, rb.velocity.y);
        }
        else if (currentState == JumpState)
        {
            rb.velocity = new Vector2(XMove * JumpMovementSpeed, rb.velocity.y);
        }
        else if (currentState == FallState)
        {
            rb.velocity = new Vector2(XMove * FallMovementSpeed, rb.velocity.y);
        }
        else if (currentState == WallgrabState)
        {
            rb.velocity = new Vector2(rb.velocity.x, YMove * ClimbSpeed);
        }
        else if (currentState == WallslideState)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -WallSlideSpeed, float.MaxValue));
        } 
        else if (currentState == DashState)
        {
            if (XMove == 0f && YMove != 0f)
            {
                rb.velocity = new Vector2(0, YMove * DashScale * 0.5f);
            }
            else if (XMove != 0f && YMove != 0f)
            {
                float dashDirection = transform.localScale.x / Mathf.Abs(transform.localScale.x);
                rb.velocity = new Vector2(dashDirection * DashScale * 0.7f, YMove * DashScale * 0.7f);
            }
            else
            {
                float dashDirection = transform.localScale.x / Mathf.Abs(transform.localScale.x);
                rb.velocity = new Vector2(dashDirection * DashScale, 0f);
            }
        }
    }

    private void GetMoveInput()
    {
        XMove = Input.GetAxisRaw("Horizontal");
        YMove = Input.GetAxisRaw("Vertical");
    }

    public void OnGroundedChange(bool _onGround)
    {
        OnGround = _onGround;
        PlayerAnimator.SetBool("onGround", _onGround);
    }

    public void OnWalledChange(bool _onWall)
    {
        OnWall = _onWall;
    }

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
                    Destroy(this.gameObject);
                    //Game Over
                    break;
                case "Spike":
                    print("DEAD");
                    Destroy(this.gameObject);
                    //Game Over
                    break;
            }
        }
    }
}
