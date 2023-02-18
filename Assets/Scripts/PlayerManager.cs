using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private BoxCollider2D bc;

    public bool onGround = false;
    public bool onWall = false;
    public bool onWallGrab = false;
    public bool isWallSliding = false;
    public bool isWallJumping = false;
    public bool isDashing = false;
    public float xMove;
    public float yMove;

    [SerializeField] private Camera playerCamera;

    // Start is called before the first frame update
    private void Awake()
    {
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        GetMoveInput();
    }

    private void FixedUpdate()
    {
        CheckTagOverlap();
    }

    private void GetMoveInput()
    {
        xMove = Input.GetAxisRaw("Horizontal");
        yMove = Input.GetAxisRaw("Vertical");
    }

    public void OnGroundedChange(bool _onGround)
    {
        onGround = _onGround;
    }

    public void OnWalledChange(bool _onWall)
    {
        onWall = _onWall;
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
