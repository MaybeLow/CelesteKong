using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderDisableCommand : BoulderCommand
{
    SpriteRenderer sr;
    CircleCollider2D circleCollider;

    public BoulderDisableCommand(IEntity entity, float time, SpriteRenderer sr, CircleCollider2D collider) : base(entity, time)
    {
        this.circleCollider = collider;
        this.sr = sr;
    }

    public override void Execute()
    {
        circleCollider.enabled = false;
        sr.enabled = false;
    }

    public override void Undo()
    {
        circleCollider.enabled = true;
        sr.enabled = true;
    }
}
