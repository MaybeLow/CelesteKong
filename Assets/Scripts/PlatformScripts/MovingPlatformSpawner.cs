using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformSpawner : MonoBehaviour
{
    private static Queue<GameObject> pool = new Queue<GameObject>();

    [SerializeField] private GameObject platformPrefab;
    private Transform tr;

    private GameObject platform;

    private void Awake()
    {
        tr = transform;
    }

    public void SpawnPlatform()
    {
        platform = GetPlatform();
        platform.transform.SetPositionAndRotation(tr.position, tr.rotation);
        platform.SetActive(true);
    }

    private GameObject GetPlatform()
    {
        if (pool.Count == 0)
        {
            platform = Instantiate(platformPrefab);
            return platform;
        }

        platform = pool.Dequeue();
        return platform;
    }

    public static void AddOnPool(GameObject movingPlatform)
    {
        pool.Enqueue(movingPlatform);
    }
}
