using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPEntity
{
    Rigidbody2D rb { get; }
    BoxCollider2D bc { get; }
    SpriteRenderer sr { get; }
}
