using UnityEngine;

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
       
        entity.rb.position = startingPosition;
    }

    public override void Undo()
    {
        if (endingPosition != null)
        {
            entity.rb.position = endingPosition;
        }
    } 
}
