using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool onGround = false;
    public bool onWall = false;
    public bool onWallGrab = false;
    public bool isWallSliding = false;
    public bool isWallJumping = false;
    public float xMove;
    public float yMove;

    // Update is called once per frame
    private void Update()
    {
        GetMoveInput();
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
}
