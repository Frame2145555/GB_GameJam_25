using UnityEngine;

public class KnifePattern : ConcreteBulletPattern
{
    [SerializeField] GameObject knifePrefab;
    [SerializeField] Transform spawnReference; // Reference GameObject for spawning position

    [SerializeField] float minSpawnInterval = 0.5f; // Minimum time between spawns
    [SerializeField] float maxSpawnInterval = 2.0f; // Maximum time between spawns
    [SerializeField] float minY = -5f; // Minimum Y position for spawning
    [SerializeField] float maxY = 5f; // Maximum Y position for spawning

    [SerializeField] int knifeCount = 30;
    float timer;
    float currentSpawnInterval;

    int knifeCounter;

    public override void PatternStart()
    {
        currentSpawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
        base.PatternStart();
    }

    public override void PatternEnd()
    {
        knifeCounter = 0;
        timer = 0;
        base.PatternEnd();
    }
    protected override void Update()
    {
        base.Update();
        // Spawn knives at random intervals
        timer += Time.deltaTime;
        if (timer >= currentSpawnInterval)
        {
            SpawnKnife();
            timer = 0;
            // Set a new random spawn interval
            currentSpawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
        }
        if (knifeCounter > knifeCount)
        {
            PatternEnd();
        }
    }

    void SpawnKnife()
    {
        // Generate a random Y position within the range
        float randomYOffset = Random.Range(minY, maxY);

        // Spawn the knife at the random Y position, at X = 0, Z = 0
        Vector3 spawnPosition = spawnReference.position + new Vector3(0, randomYOffset, 0);
        GameObject knife = Instantiate(knifePrefab, spawnPosition, Quaternion.identity);

        // Ensure the knife is pointing upward
        knife.transform.up = Vector3.up;

        knifeCounter++;
    }

    void OnDrawGizmos()
    {
        if (spawnReference != null)
        {
            // Draw the spawnable area as a vertical line
            Gizmos.color = Color.green;
            Vector3 start = spawnReference.position + new Vector3(0, minY, 0);
            Vector3 end = spawnReference.position + new Vector3(0, maxY, 0);
            Gizmos.DrawLine(start, end);

            // Draw small spheres at the min and max Y positions for clarity
            Gizmos.DrawSphere(start, 0.2f);
            Gizmos.DrawSphere(end, 0.2f);
        }
    }
}
