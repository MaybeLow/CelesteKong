using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderDisableCommand : BoulderCommand
{
    private SpriteRenderer sr;
    private CircleCollider2D circleCollider;
    private bool triggeredByPlayer;

    public BoulderDisableCommand(IEntity entity, float time, SpriteRenderer sr, CircleCollider2D collider, bool triggeredByPlayer) : base(entity, time)
    {
        circleCollider = collider;
        this.sr = sr;
        this.triggeredByPlayer = triggeredByPlayer;
    }

    public override void Execute()
    {
        if (triggeredByPlayer)
        {
            entity.breakSound.pitch = 1f;
            entity.breakSound.Play();
        }

        circleCollider.enabled = false;
        sr.enabled = false;
    }

    public override void Undo()
    {
        Debug.Log(triggeredByPlayer);
        if (triggeredByPlayer)
        {
            entity.breakSound.pitch = -1f;
            entity.breakSound.Play();
        }
        circleCollider.enabled = true;
        sr.enabled = true;
    }
}
