using UnityEngine;

public class SpikedPlatformChildCollisionDisabler : MonoBehaviour
{
    private SpriteRenderer parentSpriteRenderer;
    private BoxCollider2D bc;
    private SpriteRenderer sr;

    void Awake()
    {
        bc = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        parentSpriteRenderer = transform.parent.gameObject.GetComponent<SpriteRenderer>();
    }

    // Disable spikes if the platform is disabled
    void FixedUpdate()
    {
        if (parentSpriteRenderer.enabled == false) { 
            bc.enabled = false;
            sr.enabled = false;
        } else
        {
            bc.enabled = true;
            sr.enabled = true;
        }
    }
}
