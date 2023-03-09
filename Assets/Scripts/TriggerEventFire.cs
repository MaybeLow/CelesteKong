using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEventFire : MonoBehaviour
{
    public UnityEvent<bool> OnTriggerChange;

    public UnityEvent<MovingPlatform> OnMovingPlatformEnter;
    public UnityEvent<MovingPlatform> OnMovingPlatformExit;

    private MovingPlatform moving;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            OnTriggerChange?.Invoke(true);

            moving = collision.gameObject.GetComponentInParent<MovingPlatform>();
            if (moving)
            {
                OnMovingPlatformEnter?.Invoke(moving);
                //Debug.Log("entrer " + moving);
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            OnTriggerChange?.Invoke(false);

            if (moving)
            {
                OnMovingPlatformExit?.Invoke(moving);
                //Debug.Log("exit " + moving);
            }
        }
    }
}
