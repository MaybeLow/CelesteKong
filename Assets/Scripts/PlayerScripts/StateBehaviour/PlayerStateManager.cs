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


    public float MovementSpeed { get; set; } = 10f;
    public float JumpScale { get; set; } = 10f;
    public Rigidbody2D rb { get; set; }
    private BoxCollider2D bc;
    public bool OnGround { get; set; } = false;
    public bool OnWall { get; set; } = false;
    public bool OnWallGrab { get; set; } = false;
    public bool IsWallSliding { get; set; } = false;
    public bool IsWallJumping { get; set; } = false;
    public bool IsDashing { get; set; } = false;

    public float XMove { get; set; }
    public float YMove { get; set; }

    public Animator PlayerAnimator { get; set; }

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
        PlayerState newState = currentState.Tick(this);
        if (!newState.Equals(currentState))
        {
            currentState.Exit(this);
            currentState = newState;
            currentState.Enter(this);
        }
    }

    private void FixedUpdate()
    {
        CheckTagOverlap();
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
