using UnityEngine;
using System.Collections;

public class firstMapController : concreteBullet
{
    [SerializeField] GameObject thorn;
    public float moveSpeed = 0.1f;  // Speed at which the thorn moves (units per second)
    private Coroutine thornMovementCoroutine;
    public override void PatternStart()
    {
        base.PatternStart();
        thornMovementCoroutine = StartCoroutine(transformPosition());
    }
    public override void PatternEnd()
    {
        base.PatternEnd();
        StopCoroutine(thornMovementCoroutine);  // Stop the specific coroutine
        thornMovementCoroutine = null;
    }

    protected override void Update()
    {
        base.Update();
        //Debug.Log(thorn.transform.position.x);
        if(thorn.transform.position.x <= -52f)
        {
            PatternEnd();
        }
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