using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderEnableCommand : BoulderCommand
{
    SpriteRenderer sr;
    CircleCollider2D circleCollider;

    public BoulderEnableCommand(IEntity entity, float time, SpriteRenderer sr, CircleCollider2D collider) : base(entity, time)
    {
        this.circleCollider = collider;
        this.sr = sr;
    }

    public override void Execute()
    {
        circleCollider.enabled = true;
        sr.enabled = true;
    }

    public override void Undo()
    {
        circleCollider.enabled = false;
        sr.enabled = false;
    }
}
