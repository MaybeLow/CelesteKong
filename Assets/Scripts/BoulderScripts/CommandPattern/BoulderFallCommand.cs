using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderFallCommand : BoulderCommand
{
    private float fallSpeed = 0.1f;

    public BoulderFallCommand(IEntity entity, float time) : base(entity, time)
    {

    }

    public override void Execute()
    {
        entity.rb.MovePosition(new Vector2(entity.rb.position.x, entity.rb.position.y - fallSpeed));
    }

    public override void Undo()
    {
        entity.rb.MovePosition(new Vector2(entity.rb.position.x, entity.rb.position.y + fallSpeed));
    }
}
