using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class PlatformMoveCommand : PlatformCommand
{
    private float moveSpeed = 5.1f;
    private Vector2 moveDirection;

    public PlatformMoveCommand(IPEntity entity, float time, Vector2 moveDirection) : base(entity, time)
    {
        this.moveDirection = moveDirection;
    }
    public override void Execute()
    {
        entity.rb.velocity = new Vector2(moveDirection.x * moveSpeed, 0);
    }

    public override void Undo()
    {
        entity.rb.velocity = new Vector2(-moveDirection.x * moveSpeed, 0);
    }
}
