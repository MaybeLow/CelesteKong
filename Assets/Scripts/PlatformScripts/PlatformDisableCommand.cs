using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDisableCommand : PlatformCommand
{
    SpriteRenderer sr;

    public PlatformDisableCommand(IPEntity entity, float time, SpriteRenderer sr) : base(entity, time)
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
