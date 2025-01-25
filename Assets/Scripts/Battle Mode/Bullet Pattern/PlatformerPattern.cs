using NUnit.Framework;
using UnityEngine;

public class PlatformerPattern : ConcreteBulletPattern
{
    [Header("Reference")]
    [SerializeField] GameObject platfrom;
    [SerializeField] GameObject spike;
    [SerializeField] Transform platfromSpawnReference;
    [SerializeField] Transform spikeSpawnReference;

    [Header("Spawning")]
    [SerializeField] float spikeSpawnInterval;
    [SerializeField] float platformSpawnInterval;
    [SerializeField] MinMaxNum<float> platformSpawnRange;
    private void Start()
    {
        InvokeRepeating(nameof(SpawnSpike), 0f,spikeSpawnInterval);
        InvokeRepeating(nameof(SpawnPlatform), 0f,platformSpawnInterval);
    }
    protected override void Update()
    {
    }

    void SpawnSpike()
    {
        Instantiate(spike,spikeSpawnReference.position,Quaternion.identity);
    }
    
    void SpawnPlatform()
    {
        float randY = Random.Range(platformSpawnRange.min,platformSpawnRange.max);
        Vector3 spawnPos = new Vector3(platfromSpawnReference.position.x, platfromSpawnReference.position.y + randY, 0);

        Instantiate(platfrom, spawnPos, Quaternion.identity);
    }

}
