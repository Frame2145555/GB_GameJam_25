using UnityEngine;
using System.Collections;

public class SpikeBulletPattern : ConcreteBulletPattern
{
    [SerializeField] GameObject thorn;
    public float moveSpeed = 0.1f;  // Speed at which the thorn moves (units per second)
    private Coroutine thornMovementCoroutine;
    public override void PatternStart()
    {
        base.PatternStart();
        thornMovementCoroutine = StartCoroutine(transformPosition());
        Invoke(nameof(PatternEnd), 5);
    }
    public override void PatternEnd()
    {
        StopCoroutine(thornMovementCoroutine);  // Stop the specific coroutine
        thornMovementCoroutine = null;
        base.PatternEnd();
    }

    protected override void Update()
    {
    }

    private IEnumerator transformPosition()
    {
        // Move the thorn smoothly to the left over time
        while (true)
        {
            // Move left by a small amount each frame
            thorn.transform.position += new Vector3(-moveSpeed * Time.deltaTime, 0f, 0f);

            // Wait for the next frame to make the movement smooth
            yield return null;
        }
    }
}