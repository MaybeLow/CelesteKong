using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerManager pm;

    private float currentCooldown = 0;
    private float dashTime = 0.3f;
    private bool isRecharged = true;

    [SerializeField] private float dashScale = 500f;
    [SerializeField] private float cooldownTime = 3.0f;

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
        //DashCooldown();
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
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        rb.velocity = new Vector2(transform.localScale.x / Mathf.Abs(transform.localScale.x) * dashScale, 0f);
        isRecharged = false;
        pm.isDashing = true;
        float savedGravity = rb.gravityScale;
        rb.gravityScale = 0;
        currentCooldown = cooldownTime;

        yield return new WaitForSeconds(dashTime);
        pm.isDashing = false;
        rb.gravityScale = savedGravity;

        yield return new WaitForSeconds(cooldownTime);
        isRecharged = true;
    }
}
