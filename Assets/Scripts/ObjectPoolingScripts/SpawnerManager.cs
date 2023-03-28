using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    private Queue<GameObject> pool = new Queue<GameObject>();

    [SerializeField] private Spawner[] spawners;
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
        //Debug.Log(nextBoulderIn);
        if (!GameManager.UndoActive())
        {
            nextBoulderIn -= Time.fixedDeltaTime;
            if (nextBoulderIn <= 0f)
            {
                SpawnNextPlatform();
                nextBoulderIn = spawnDelay;
            }
        } 
        else
        {
            nextBoulderIn += Time.fixedDeltaTime;
            if (nextBoulderIn >= spawnDelay)
            {
                nextBoulderIn = 0f;
            }
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
