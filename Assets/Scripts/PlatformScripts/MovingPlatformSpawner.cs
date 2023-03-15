using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformSpawner : MonoBehaviour
{
    private Queue<GameObject> pool;

    [SerializeField] private GameObject platformPrefab;
    private Transform tr;

    private GameObject platform;

    private void Awake()
    {
        pool = transform.parent.gameObject.GetComponent<MovingPlatformSpawnerManager>().GetPool();
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
            platform.GetComponent<MovingPlatform>().AssignSpawner(this)
;           return platform;
        }

        platform = pool.Dequeue();
        return platform;
    }

    public void AddOnPool(GameObject movingPlatform)
    {
        pool.Enqueue(movingPlatform);
    }
}
