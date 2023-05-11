using UnityEngine;

public class PlatformEnableCommand : PlatformCommand
{
    private Vector2 velocity;

    public PlatformEnableCommand(IPEntity entity, float time, SpriteRenderer sr, BoxCollider2D bc, Vector2 velocity) : base(entity, time)
    {
        this.velocity = velocity;
    }

    public override void Execute()
    {
        entity.sr.enabled = true;
        entity.bc.enabled = true;
    }

    public override void Undo()
    {
        entity.rb.velocity = Vector2.zero;
        entity.sr.enabled = false;
        entity.bc.enabled = false;
    }
}
