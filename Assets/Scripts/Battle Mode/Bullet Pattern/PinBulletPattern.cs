using UnityEngine;
using System.Collections;

public class PinBulletPattern : ConcreteBulletPattern
{
    [SerializeField] GameObject toInstantiate;         // Object to spawn
    [SerializeField] float spawnInterval = 1f;         // Time between spawns
    [SerializeField] GameObject[] spawnPositions;      // Array of spawn points (GameObjects)
    private Coroutine spawnCoroutine;                   // Reference to the coroutine

    public override void PatternStart()
    {
        base.PatternStart();

        // If the coroutine is not already running, start it
        if (spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(SpawnBullets());
        }
    }

    public override void PatternEnd()
    {

        // If the coroutine is running, stop it
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
        base.PatternEnd();
    }

    private IEnumerator SpawnBullets()
    {
        // Keep spawning bullets at the assigned spawn positions indefinitely (or until PatternEnd is called)
        while (true)
        {
            SpawnBulletsAtPositions();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnBulletsAtPositions()
    {
        // Loop through each spawn position and instantiate a bullet there
        foreach (GameObject spawnPoint in spawnPositions)
        {
            if (spawnPoint != null)
            {
                // Use the spawn point's position
                Vector2 spawnPosition = spawnPoint.transform.position;

                // Instantiate a bullet at each spawn position
                if (toInstantiate != null)
                {
                    GameObject bullet = Instantiate(toInstantiate, spawnPosition, Quaternion.identity);

                    // Optionally, you can modify the bullet's behavior or direction here if needed
                    bullet bulletScript = bullet.GetComponent<bullet>();
                    if (bulletScript != null)
                    {
                        bulletScript.speed = 5f;  // Set the speed of the bullet (you can adjust this)
                    }

                    //Debug.Log("Bullet spawned at position: " + spawnPosition);
                }
                else
                {
                    Debug.LogWarning("No object assigned to instantiate!");
                }
            }
        }
    }

    protected override void Update()
    {
    }
}