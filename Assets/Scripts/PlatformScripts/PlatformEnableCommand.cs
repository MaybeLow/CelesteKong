using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEnableCommand : PlatformCommand
{
    private SpriteRenderer sr;
    private BoxCollider2D bc;
    private Vector2 velocity;

    public PlatformEnableCommand(IPEntity entity, float time, SpriteRenderer sr, BoxCollider2D bc, Vector2 velocity) : base(entity, time)
    {
        this.sr = sr;
        this.bc = bc;
        this.velocity = velocity;
    }

    public override void Execute()
    {
        entity.rb.velocity = velocity;
        sr.enabled = true;
        bc.enabled = true;
    }

    public override void Undo()
    {
        entity.rb.velocity = Vector2.zero;
        sr.enabled = false;
        bc.enabled = false;
    }
}
