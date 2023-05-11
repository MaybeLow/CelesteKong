using UnityEngine;
using UnityEngine.Events;

public class TriggerEventFire : MonoBehaviour
{
    public UnityEvent<bool> OnTriggerChange;

    public UnityEvent<MovingPlatform> OnMovingPlatformEnter;
    public UnityEvent<MovingPlatform> OnMovingPlatformExit;

    private MovingPlatform moving;

    // Check when the player stands on the ground.
    // Also update the player's velocity according to the velocity of the moving platform it stands on
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")
            || (collision.CompareTag("Poolable") && GameManager.UndoActive()))
        {
            OnTriggerChange?.Invoke(true);

            moving = collision.gameObject.GetComponentInParent<MovingPlatform>();
            if (moving)
            {
                OnMovingPlatformEnter?.Invoke(moving);
            }

        }
    }

    // Check when the player leaves the ground.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")
            || (collision.CompareTag("Poolable") && GameManager.UndoActive()))
        {
            OnTriggerChange?.Invoke(false);

            if (moving)
            {
                OnMovingPlatformExit?.Invoke(moving);
            }
        }
    }
}
