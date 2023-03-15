using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderSpawnerManager : MonoBehaviour
{
    private Queue<GameObject> pool = new Queue<GameObject>();

    [SerializeField] private BoulderSpawner[] spawners;
    [SerializeField] private float spawnDelay;

    private int currentSpawner;

    private float nextBoulderIn;

    private void Start()
    {
        currentSpawner = -1;
        nextBoulderIn = 0f;
    }

    private void FixedUpdate()
    {
        nextBoulderIn -= Time.fixedDeltaTime;
        if (nextBoulderIn <= 0f)
        {
            SpawnNextPlatform();
            nextBoulderIn = spawnDelay;
        }
    }

    public Queue<GameObject> GetPool()
    {
        return pool;
    }

    private void SpawnNextPlatform()
    {
        currentSpawner++;

        if (currentSpawner >= spawners.Length)
        {
            currentSpawner = 0;
        }

        spawners[currentSpawner].SpawnBoulder();
    }
}
