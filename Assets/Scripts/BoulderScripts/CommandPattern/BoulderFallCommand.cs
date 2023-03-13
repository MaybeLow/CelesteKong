using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderFallCommand : BoulderCommand
{
    private float fallSpeed = 3f;

    public BoulderFallCommand(IEntity entity, float time) : base(entity, time)
    {

    }

    public override void Execute()
    {
        entity.rb.velocity = new Vector2(0f, -fallSpeed);
    }

    public override void Undo()
    {
        entity.rb.velocity = new Vector2(0f, fallSpeed);
    }
}
