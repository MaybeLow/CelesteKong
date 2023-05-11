using UnityEngine;
using UnityEngine.Events;

public class BoulderGroundEvent : MonoBehaviour
{
    public UnityEvent<bool> OnTriggerChange;

    // Check if the boulder hits the ground. 
    // Each boulder considers most of the objects as grounds
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Spike") 
            && !collision.CompareTag("Spike") && collision.gameObject.layer != LayerMask.NameToLayer("Boundary"))
        {
            OnTriggerChange?.Invoke(true);
        }
    }

    // Check if the boulder leaves the ground. 
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Spike")
            && !collision.CompareTag("Spike") && collision.gameObject.layer != LayerMask.NameToLayer("Boundary"))
        {
            OnTriggerChange?.Invoke(false);
        }
    }
}
