using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderSpawner : MonoBehaviour
{
    private float targetTime;
    [SerializeField] private float cooldownTime = 15.0f;
    [SerializeField] private Boulder boulderPrefab;
    [SerializeField] private bool faceRight = false;

    private void Start()
    {
        targetTime = cooldownTime;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        cooldownTime -= Time.deltaTime;
        if (cooldownTime <= 0.0f)
        {
            SpawnBoulder();
            cooldownTime = targetTime;
        }
    }

    private void SpawnBoulder()
    {
        //boulderPrefab.SetDirection(faceRight);
        //Boulder boulder = Instantiate(boulderPrefab, transform.position, Quaternion.identity);

        //boulder.SetDirection(faceRight);
    }
}
