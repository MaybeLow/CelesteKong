using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private float startDelay;
    [SerializeField] private float endDelay = 4.0f;

    private MovingPlatform platform;

    private void Awake()
    {
        platform = GetComponent<MovingPlatform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("ObjectPooler") || collision.CompareTag("PlatformDestroyer")) && !GameManager.UndoActive())
        {
            if (!platform.IsTeleporting())
            {
                platform.Teleport();
            }
        }
        else if ((collision.CompareTag("ObjectPooler") || collision.CompareTag("PlatformSpawner")) && GameManager.UndoActive())
        {
            if (!platform.IsTeleporting())
            {
                platform.Teleport();
            }
        }
    }
}
