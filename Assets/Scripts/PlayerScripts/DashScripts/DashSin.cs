using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DashSin : PlayerDash
{
    protected override void DashEquation()
    {
        if (pm.IsDashing)
        {
            dashMovement.x = dashDirection * dashRelativePosition.x;
            //dashMovement.y = DashFunction(dashMovement.x);
            dashMovement.y = DashFunction(dashMovement.x);
            if (Mathf.Abs(DashFunction(0)) <= 0.5f) {
                rb.MovePosition(new Vector2(dashMovement.x + dashStartingPosition.x, dashMovement.y + dashStartingPosition.y));

                //find the vector pointing from our position to the target
                Vector2 direction = new Vector2(dashMovement.x, dashMovement.y);

                //create the rotation we need to be in to look at the target
                Quaternion lookRotation = Quaternion.LookRotation(direction);

                //rotate us over time according to speed until we are in the required rotation
                transform.rotation = lookRotation;

                dashRelativePosition.x += 0.2f;
            } else
            {
                print("f(0) = " + dashMovement.y);
            }
        }
    }

    private float DashFunction(float x)
    {
        return Mathf.Sin(x * 0.2f) * 5;
        //return Mathf.Log(x * 0.2f) * 5;
    }
}
