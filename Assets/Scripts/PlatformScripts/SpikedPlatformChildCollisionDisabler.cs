using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedPlatformChildCollisionDisabler : MonoBehaviour
{
    private SpriteRenderer parentSpriteRenderer;
    private BoxCollider2D bc;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Awake()
    {
        bc = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        parentSpriteRenderer = transform.parent.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
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
