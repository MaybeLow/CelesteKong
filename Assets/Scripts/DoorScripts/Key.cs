using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private Door attachedDoor;
    // Start is called before the first frame update
    private void Start()
    {
        attachedDoor = GetComponentInParent<Door>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player_")) {
            gameObject.SetActive(false);
            attachedDoor.Deactivate();
        }
    }
}
