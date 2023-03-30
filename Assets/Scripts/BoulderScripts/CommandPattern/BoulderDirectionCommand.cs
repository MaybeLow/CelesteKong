using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class BoulderDirectionCommand : BoulderCommand
{
    private Boulder boulder;

    public BoulderDirectionCommand(IEntity entity, float time, Boulder boulder) : base(entity, time)
    {
        this.boulder = boulder;
    }

    public override void Execute()
    {
        boulder.ChangeBoulderDirection();
    }

    public override void Undo()
    {
        boulder.ChangeBoulderDirection();
    }
}
