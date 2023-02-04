using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float targetTime;
    [SerializeField] private float cooldownTime = 15.0f;
    [SerializeField] private Boulder boulderPrefab;

    private void Start()
    {
        targetTime = cooldownTime;
    }

    // Update is called once per frame
    private void Update()
    {
        cooldownTime -= Time.deltaTime;
        if (cooldownTime <= 0.0f)
        {
            spawnBoulder();
        }
    }

    private void spawnBoulder()
    {
        cooldownTime = targetTime;
        Instantiate(boulderPrefab, transform.position, Quaternion.identity);
    }
}
