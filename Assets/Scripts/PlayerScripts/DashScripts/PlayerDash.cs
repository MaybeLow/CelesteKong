using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerManager))]

public abstract class PlayerDash : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected PlayerManager pm;

    protected float dashTime = 1.3f;
    protected bool isRecharged = true;
    protected float currentX;
    protected Vector2 dashRelativePosition;
    protected Vector2 dashStartingPosition;
    protected float dashDirection;
    protected Vector2 dashMovement;

    [SerializeField] protected float dashScale = 10f;
    [SerializeField] protected float cooldownTime = 3.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pm = GetComponent<PlayerManager>();
    }

    private void Start()
    {
        //dashStartingPosition = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        CheckInput();
    }

    private void FixedUpdate()
    {
        DashEquation();
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
        pm.IsDashing = true;

        dashDirection = transform.localScale.x / Mathf.Abs(transform.localScale.x);
        dashRelativePosition = new Vector2(0f, 0f);
        dashStartingPosition = transform.position;

        isRecharged = false;
        pm.PlayerAnimator.SetBool("isDashing", true);
        float savedGravity = rb.gravityScale;
        rb.gravityScale = 0;

        yield return new WaitForSeconds(dashTime);
        transform.rotation = Quaternion.identity;
        pm.IsDashing = false;
        pm.PlayerAnimator.SetBool("isDashing", false);
        rb.gravityScale = savedGravity;

        yield return new WaitForSeconds(cooldownTime);
        isRecharged = true;
    }

    protected abstract void DashEquation();
}
