using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class PlatformTeleportCommand : PlatformCommand
{
    private Vector2 startingPosition;
    private Vector2 endingPosition;
    private MovingPlatform platform;

    public PlatformTeleportCommand(IPEntity entity, float time, Vector2 startingPosition, Vector2 endingPosition, MovingPlatform platform) : base(entity, time)
    {
        this.startingPosition = startingPosition;
        this.endingPosition = endingPosition;
        this.platform = platform;
    }
    public override void Execute()
    {
        //if (!platform.IsTeleporting())
        //{
            entity.rb.position = startingPosition;
            /*platform.DisablePlatform();
            platform.StartTeleport();*/
        //}
    }

    public override void Undo()
    {
        if (endingPosition != null/* && !platform.IsTeleporting()*/)
        {
            entity.rb.position = endingPosition;
            //platform.DisablePlatform();
            //platform.StartTeleport();
        }
    } 
}
