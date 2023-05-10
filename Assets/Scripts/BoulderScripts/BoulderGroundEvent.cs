using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoulderGroundEvent : MonoBehaviour
{
    public UnityEvent<bool> OnTriggerChange;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Spike") 
            && !collision.CompareTag("Spike") && collision.gameObject.layer != LayerMask.NameToLayer("Boundary"))
        {
            OnTriggerChange?.Invoke(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Spike")
            && !collision.CompareTag("Spike") && collision.gameObject.layer != LayerMask.NameToLayer("Boundary"))
        {
            OnTriggerChange?.Invoke(false);
        }
    }
}
