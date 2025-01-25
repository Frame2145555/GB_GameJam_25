using UnityEngine;
using System.Collections;

public class bullet1 : concreteBullet
{
    [SerializeField] GameObject toInstantiate; // Object to spawn
    [SerializeField] float spawnInterval = 1f; // Time between spawns
    private bool spawnCoroutine; // Reference to the coroutine

    public override void PatternStart()
    {
        base.PatternStart();
        if (spawnCoroutine)
        {
            StartCoroutine(SpawnBullets());
        }
    }

    public override void PatternEnd()
    {
        base.PatternEnd();
        if (!spawnCoroutine)
        {
            StopCoroutine(SpawnBullets());
        }
    }

    private IEnumerator SpawnBullets()
    {
        while (true)
        {
            SpawnBullet();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnBullet()
    {
        if (toInstantiate != null)
        {
            Instantiate(toInstantiate, transform.position, Quaternion.identity);
            Debug.Log("Bullet spawned");
        }
        else
        {
            Debug.LogWarning("No object assigned to instantiate!");
        }
    }
}
