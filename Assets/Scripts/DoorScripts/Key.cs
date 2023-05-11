using UnityEngine;

public class Key : MonoBehaviour
{
    private Door attachedDoor;

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
