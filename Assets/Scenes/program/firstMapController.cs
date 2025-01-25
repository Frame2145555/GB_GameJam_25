using UnityEngine;
using System.Collections;

public class firstMapController : MonoBehaviour
{
    [SerializeField] GameObject thorn;
    public float moveSpeed = 0.1f;  // Speed at which the thorn moves (units per second)

    private void Start()
    {
        StartCoroutine(transformPosition());
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