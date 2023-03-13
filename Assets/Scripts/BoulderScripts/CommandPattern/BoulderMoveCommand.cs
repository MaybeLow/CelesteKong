using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderMoveCommand : BoulderCommand
{
    private float moveSpeed = 3f;
    private Vector2 moveDirection;

    public BoulderMoveCommand(IEntity entity, float time, Vector2 moveDirection) : base(entity, time)
    {
        this.moveDirection = moveDirection;
    }

    public override void Execute()
    {
        entity.rb.velocity = new Vector2(moveSpeed, 0f) * moveDirection;
    }

    public override void Undo()
    {
        entity.rb.velocity = new Vector2(-moveSpeed, 0f) * moveDirection;
    }
}
