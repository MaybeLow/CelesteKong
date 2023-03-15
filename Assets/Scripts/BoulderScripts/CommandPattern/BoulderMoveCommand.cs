using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderMoveCommand : BoulderCommand
{
    private float moveSpeed = 0.1f;
    private Vector2 moveDirection;

    public BoulderMoveCommand(IEntity entity, float time, Vector2 moveDirection) : base(entity, time)
    {
        this.moveDirection = moveDirection;
    }

    public override void Execute()
    {
        entity.rb.MovePosition(new Vector2(entity.rb.position.x * moveDirection.x + moveSpeed, entity.rb.position.y));
    }

    public override void Undo()
    {
        entity.rb.MovePosition(new Vector2(entity.rb.position.x * moveDirection.x - moveSpeed, entity.rb.position.y));
    }
}