using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildCollisionDisabler : MonoBehaviour
{
    private SpriteRenderer parentSpriteRenderer;
    private BoxCollider2D bc;
    // Start is called before the first frame update
    void Awake()
    {
        bc = GetComponent<BoxCollider2D>();
        parentSpriteRenderer = transform.parent.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (parentSpriteRenderer.enabled == false) { 
            bc.enabled = false;
        } else
        {
            bc.enabled = true;
        }
    }
}
