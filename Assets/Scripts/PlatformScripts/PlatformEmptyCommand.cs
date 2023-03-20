using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEmptyCommand : PlatformCommand
{
    public PlatformEmptyCommand(IPEntity entity, float time) : base(entity, time)
    {
    }

    public override void Execute()
    {
    }

    public override void Undo()
    {
    }
}
