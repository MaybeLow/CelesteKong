using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    [SerializeField] private float followSpeed = 2f;

    private Vector2 menuCentre;

    // Update menu camera
    void Update()
    {
        if (menuCentre != null && (transform.position.x, transform.position.y) != (menuCentre.x, menuCentre.y))
        {
            transform.position = Vector3.Slerp(transform.position, new Vector3(menuCentre.x, menuCentre.y, transform.position.z), followSpeed * Time.deltaTime);
        }
    }

    public void UpdateCentre(Vector2 newCentre)
    {
        menuCentre = newCentre;
    }
}
