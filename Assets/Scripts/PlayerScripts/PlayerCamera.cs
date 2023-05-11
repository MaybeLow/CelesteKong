using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float followSpeed = 2f;
    [SerializeField] private float yOffset = 1f;

    // Update is called once per frame
    void Update()
    {
        if (player != null && transform.position.x != player.position.x)
        {
            transform.position = Vector3.Slerp(transform.position, new Vector3(player.position.x, player.position.y + yOffset, transform.position.z), followSpeed * Time.deltaTime);
        }
    }
}
