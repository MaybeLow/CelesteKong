using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerManager))]

public class PlayerWallGrab : MonoBehaviour
{
    private float climbSpeed = 5;

    private Rigidbody2D rb;
    private PlayerManager pm;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pm = GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        WallGrub();
    }

    private void FixedUpdate()
    {

        WallClimb();
    }

    private void WallGrub()
    {
        if (Input.GetKey("z") && pm.OnWall)
        {
            pm.OnWallGrab = true;
        }
        else
        {
            pm.OnWallGrab = false;
        }
    }

    private void WallClimb()
    {
        // Update vertical movement
        if (pm.OnWallGrab)
        {
            rb.velocity = new Vector2(rb.velocity.x, pm.YMove * climbSpeed);
        }
    } 
}
