using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private float dashScale;
    [SerializeField] private float cooldownTime = 3.0f;
    private float currentCooldown = 0;
    private bool isRecharged = true;

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }


    // Update is called once per frame
    private void FixedUpdate()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0.0f)
        {
            isRecharged = true;
        }
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown("x") && isRecharged)
        {
            transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z) * dashScale;
            isRecharged = false;
            currentCooldown = cooldownTime;
        }
    }
}
