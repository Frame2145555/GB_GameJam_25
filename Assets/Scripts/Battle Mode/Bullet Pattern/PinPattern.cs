using UnityEngine;

public class PinPattern : ConcreteBulletPattern
{
    [Header("Spawn Settings")]
    [SerializeField] GameObject objectsToSpawn;
    [SerializeField] GameObject referenceObject;
    [SerializeField] int minObjectsPerBatch = 3;  
    [SerializeField] int maxObjectsPerBatch = 10; 
    [SerializeField] float radius = 5f;           
    [SerializeField] float positionNoise = 1f;    
    [SerializeField] float spawnInterval = 2f;    
    [SerializeField] int pinBatchCount = 6;
    [SerializeField] float centerBiasFactor = 0.5f; 


    int pinBatchCounter = 0;
     private float timer;


    public override void PatternStart()
    {
        base.PatternStart();
    }

    public override void PatternEnd()
    {
        base.PatternEnd();
    }
    protected override void Update()
    {
        // Spawn knives at random intervals
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnBatch();
            timer = 0;
            // Set a new random spawn interval
        }
        if (pinBatchCounter > pinBatchCount)
        {
            PatternEnd();
        }
    }

    void SpawnBatch()
    {
        pinBatchCounter++;
        // Determine a random number of objects to spawn in this batch
        int objectsToSpawnInBatch = Random.Range(minObjectsPerBatch, maxObjectsPerBatch + 1);

        for (int i = 0; i < objectsToSpawnInBatch; i++)
        {
            // Randomize angle for each object in 2D (X-Y plane)
            float angle = Random.Range(0f, Mathf.PI * 2);

            // Introduce a bias that makes the angle more likely to point inward
            angle += Random.Range(-centerBiasFactor, centerBiasFactor); // Bias the angle

            // Determine the base position around the reference object in 2D
            Vector3 basePosition = new Vector3(
                Mathf.Cos(angle) * radius,
                Mathf.Sin(angle) * radius,
                0f  // Set Z to 0 for 2D
            );

            // Add random noise to make the position less perfect
            Vector3 randomOffset = new Vector3(
                Random.Range(-positionNoise, positionNoise),
                Random.Range(-positionNoise, positionNoise),
                0f // No vertical noise in 2D, Z remains 0
            );

            Vector3 finalPosition = referenceObject.transform.position + basePosition + randomOffset;

            // Calculate the direction pointing toward the center (reference object)
            Vector3 directionToCenter = (referenceObject.transform.position - finalPosition).normalized;

            // Generate a rotation that points inward in 2D (around the Z axis)
            float angleToCenter = Mathf.Atan2(directionToCenter.y, directionToCenter.x) * Mathf.Rad2Deg;
            Quaternion randomRotation = Quaternion.Euler(0f, 0f, angleToCenter);

            // Choose a random object from the array
            GameObject randomObject = objectsToSpawn;

            // Instantiate the object at the calculated position with the inward rotation
            Instantiate(randomObject, finalPosition, randomRotation);
        }
    }

    // Draw gizmos to visualize the spawn area
    void OnDrawGizmos()
    {
        if (referenceObject != null)
        {
            // Draw the circular spawn area around the reference object
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(referenceObject.transform.position, radius);

            // Draw a sample of the spawn positions
            Gizmos.color = Color.red;
            for (int i = 0; i < 10; i++) // Draw 10 sample positions
            {
                float angle = Random.Range(0f, Mathf.PI * 2);
                Vector3 position = new Vector3(
                    Mathf.Cos(angle) * radius,
                    Mathf.Sin(angle) * radius,
                    0f
                );
                // Add random offset to make the positions less perfect
                position += new Vector3(
                    Random.Range(-positionNoise, positionNoise),
                    Random.Range(-positionNoise, positionNoise),
                    0f
                );
                Gizmos.DrawSphere(referenceObject.transform.position + position, 0.1f); // Draw a small sphere to represent a spawn point
            }
        }
    }
}
