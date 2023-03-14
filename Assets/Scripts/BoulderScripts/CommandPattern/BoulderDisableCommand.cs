using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderDisableCommand : BoulderCommand
{
    SpriteRenderer sr;
    public BoulderDisableCommand(IEntity entity, float time, SpriteRenderer sr) : base(entity, time)
    {
        this.sr = sr;
    }

    public override void Execute()
    {
        sr.enabled = false;
    }

    public override void Undo()
    {
        sr.enabled = true;
    }
}
