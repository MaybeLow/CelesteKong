using System.Collections;
using UnityEngine;

public class BoulderDisableCommand : BoulderCommand
{
    private SpriteRenderer sr;
    private CircleCollider2D circleCollider;
    private bool triggeredByPlayer;
    private Boulder boulder;

    public BoulderDisableCommand(IEntity entity, float time, SpriteRenderer sr, CircleCollider2D collider, bool triggeredByPlayer, Boulder boulder) : base(entity, time)
    {
        circleCollider = collider;
        this.sr = sr;
        this.triggeredByPlayer = triggeredByPlayer;
        this.boulder = boulder;
    }

    public override void Execute()
    {
        if (triggeredByPlayer)
        {
            //entity.breakSound.pitch = 1f;
            entity.breakSound.Play();
        }

        circleCollider.enabled = false;
        sr.enabled = false;
    }

    public override void Undo()
    {
        if (triggeredByPlayer)
        {
            boulder.StartCoroutine(PlayAudioInReverse());
        }
        circleCollider.enabled = true;
        sr.enabled = true;
    }

    private IEnumerator PlayAudioInReverse()
    {
        entity.breakSound.loop = true;
        entity.breakSound.pitch = -1f;
        entity.breakSound.Play();
        yield return new WaitForSeconds(entity.breakSound.clip.length);
        entity.breakSound.loop = false;
        entity.breakSound.Stop();
        entity.breakSound.pitch = 1f;
    }
}
