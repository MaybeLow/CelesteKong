using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    [SerializeField] private float followSpeed = 2f;

    private Vector2 menuCentre;

    // Update is called once per frame
    void Update()
    {
        if (menuCentre != null && transform.position.x != menuCentre.x)
        {
            transform.position = Vector3.Slerp(transform.position, new Vector3(menuCentre.x, menuCentre.y, transform.position.z), followSpeed * Time.deltaTime);
        }
    }

    public void UpdateCentre(Vector2 newCentre)
    {
        menuCentre = newCentre;
    }
}
