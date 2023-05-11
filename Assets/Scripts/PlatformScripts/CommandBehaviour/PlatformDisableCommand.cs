using UnityEngine;

public class PlatformDisableCommand : PlatformCommand
{
    private Vector2 velocity;

    public PlatformDisableCommand(IPEntity entity, float time, SpriteRenderer sr, BoxCollider2D bc, Vector2 velocity) : base(entity, time)
    {
        this.velocity = velocity;
    }

    public override void Execute()
    {
        entity.rb.velocity = Vector2.zero;
        entity.sr.enabled = false;
        entity.bc.enabled = false;
    }

    public override void Undo()
    {
        entity.sr.enabled = true;
        entity.bc.enabled = true;
    }
}
