using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerManager pm;
    [SerializeField] private float dashScale = 500f;
    [SerializeField] private float cooldownTime = 3.0f;
    private float currentCooldown = 0;
    private bool isRecharged = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pm = GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        CheckInput();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        DashCooldown();
    }

    private void DashCooldown()
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
            rb.AddForce(new Vector2(pm.xMove, pm.yMove) * dashScale);
            isRecharged = false;
            currentCooldown = cooldownTime;
        }
    }
}
