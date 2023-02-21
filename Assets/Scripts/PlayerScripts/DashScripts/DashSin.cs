using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSin : PlayerDash
{
    protected override void DashEquation()
    {
        if (pm.IsDashing)
        {
            dashMovement.x = dashDirection * dashRelativePosition.x;
            dashMovement.y = Mathf.Sin(dashMovement.x * 0.2f) * 5;
            //dashMovement.y = Mathf.Log(dashMovement.x * 0.2f) * 5;
            print("x: " + dashMovement.x);
            print("y: " + dashMovement.y);
            rb.MovePosition(new Vector2(dashMovement.x + dashStartingPosition.x, dashMovement.y + dashStartingPosition.y));
            dashRelativePosition.x += 0.2f;
        }
    }
}
