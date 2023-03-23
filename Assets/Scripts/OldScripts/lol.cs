/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lol : MonoBehaviour, IPEntity
{
    private PlatformCommandController controller;

    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    protected BoxCollider2D bc;

    private Vector2 moveDirection = Vector2.right;

    Rigidbody2D IPEntity.rb => rb;

    [SerializeField] private Vector2 velocity;

    private Vector2 startingPosition;

    private Vector2 endingPosition;

    private void Start()
    {
        startingPosition = transform.position;
    }

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<PlatformCommandController>();
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (controller.IsEmpty())
        {
            rb.velocity = Vector2.zero;
        }

        if (sr.enabled)
        {
            if (GameManager.UndoActive())
            {
                UpdateUndo();
            }
            else
            {
                UpdateMovement();
            }
        }
        else if (GameManager.UndoActive())
        {
            UpdateUndo();
        }
        else
        {
            controller.ExecuteCommand(new PlatformEmptyCommand(this, Time.timeSinceLevelLoad));
        }
    }

    private void UpdateMovement()
    {
        controller.ExecuteCommand(new PlatformMoveCommand(this, Time.timeSinceLevelLoad, moveDirection));
    }

    private void UpdateUndo()
    {
        controller.UndoCommand();
    }

    public IEnumerator DisablePlatform()
    {
        controller.ExecuteCommand(new PlatformDisableCommand(this, Time.timeSinceLevelLoad, sr, bc, velocity));
        yield return new WaitForSeconds(4.0f);
        Teleport();
        controller.ExecuteCommand(new PlatformEnableCommand(this, Time.timeSinceLevelLoad, sr, bc, velocity));
    }

    public void EnablePlatform()
    {
        controller.ExecuteCommand(new PlatformEnableCommand(this, Time.timeSinceLevelLoad, sr, bc, velocity));
    }

    public void Teleport()
    {
        endingPosition = transform.position;
        controller.ExecuteCommand(new PlatformTeleportCommand(this, Time.timeSinceLevelLoad, startingPosition, endingPosition));
    }

    public Vector2 GetVelocity()
    {
        return rb.velocity;
    }
}
*/