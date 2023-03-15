using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Queue<GameObject> pool;

    [SerializeField] private GameObject boulderPrefab;

    private Transform tr;

    private GameObject boulder;

    private void Awake()
    {
        pool = transform.parent.gameObject.GetComponent<SpawnerManager>().GetPool();
        tr = transform;
    }

    public void SpawnBoulder()
    {
        boulder = GetBoulder();
        boulder.transform.SetPositionAndRotation(tr.position, tr.rotation);
        boulder.SetActive(true);
    }

    private GameObject GetBoulder()
    {
        if (pool.Count == 0)
        {
            boulder = Instantiate(boulderPrefab);
            boulder.SendMessage("AssignSpawner", this);
            return boulder;
        }

        boulder = pool.Dequeue();
        return boulder;
    }

    public void AddOnPool(GameObject boulder)
    {
        pool.Enqueue(boulder);
    }
}
