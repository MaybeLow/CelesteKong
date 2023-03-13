using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour, IEntity
{
    private BoulderCommandController controller;
    private Vector2 moveDirection;
    private bool undoActive;

    protected Rigidbody2D rb;

    [SerializeField] private Transform groundChecker;
    [SerializeField] private LayerMask groundLayer;

    Rigidbody2D IEntity.rb => rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<BoulderCommandController>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        moveDirection = Vector2.right;
        undoActive = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (undoActive)
        {
            UpdateUndo();
        } else
        {
            UpdateMovement();
        }
    }

    private void UpdateMovement() {
        if (IsGrounded())
        {
            controller.ExecuteCommand(new BoulderMoveCommand(this, Time.timeSinceLevelLoad, moveDirection));
        }
        else
        {
            controller.ExecuteCommand(new BoulderFallCommand(this, Time.timeSinceLevelLoad));
        }
    }

    private void UpdateUndo()
    {
        controller.UndoCommand();
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundChecker.position, 0.5f, groundLayer);
    }
}
