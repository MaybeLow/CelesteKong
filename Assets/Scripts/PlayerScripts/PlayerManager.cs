using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class PlayerManager : MonoBehaviour
{
    private BoxCollider2D bc;

    public bool OnGround { get; set; } = false;
    public bool OnWall { get; set; } = false;
    public bool OnWallGrab { get; set; } = false;
    public bool IsWallSliding { get; set; } = false;
    public bool IsWallJumping { get; set; } = false;
    public bool IsDashing { get; set; } = false;

    public float XMove { get; set; }
    public float YMove { get; set; }

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
        XMove = Input.GetAxisRaw("Horizontal");
        YMove = Input.GetAxisRaw("Vertical");
    }

    public void OnGroundedChange(bool _onGround)
    {
        OnGround = _onGround;
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
