using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEnableCommand : PlatformCommand
{
    SpriteRenderer sr;

    public PlatformEnableCommand(IPEntity entity, float time, SpriteRenderer sr) : base(entity, time)
    {
        this.sr = sr;
    }

    public override void Execute()
    {
        sr.enabled = true;
    }

    public override void Undo()
    {
        sr.enabled = false;
    }
}
