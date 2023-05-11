using UnityEngine;

public class ChildCollisionDisabler : MonoBehaviour
{
    private SpriteRenderer parentSpriteRenderer;
    private BoxCollider2D bc;

    void Awake()
    {
        bc = GetComponent<BoxCollider2D>();
        parentSpriteRenderer = transform.parent.gameObject.GetComponent<SpriteRenderer>();
    }

    // Hide the object if the parent is hidden
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
