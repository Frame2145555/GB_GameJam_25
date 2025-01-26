using UnityEngine;
using System.Collections;

public class PlatformBulletPattern : ConcreteBulletPattern
{
    [SerializeField] GameObject[] floor;  // Array of floor objects to move
    [SerializeField] float moveSpeed = 2f;  // Speed of the movement
    [SerializeField] float moveDistance = 5f;  // The distance the floor objects will move up and down
    [SerializeField] float startDelay = 0.5f;  // Delay between the start of each floor's movement
    private Vector3[] initialPositions;  // Store the initial positions of the floor objects
    private bool[] movingUp;  // Track whether each floor is moving up or down
    private Coroutine movementCoroutine;  // Reference to the coroutine

    public override void PatternStart()
    {
        base.PatternStart();

        // Initialize positions and movement directions
        initialPositions = new Vector3[floor.Length];
        movingUp = new bool[floor.Length];

        for (int i = 0; i < floor.Length; i++)
        {
            initialPositions[i] = floor[i].transform.position;  // All floors start at the same position
            movingUp[i] = (i % 2 == 0);  // Even index floors move up, odd index floors move down
        }
        // Start the movement coroutine
        if (movementCoroutine == null)
        {
            movementCoroutine = StartCoroutine(StartFloorMovementWithDelay());
        }
    }

    public override void PatternEnd()
    {
        base.PatternEnd();
        // Stop the movement coroutine
        if (movementCoroutine != null)
        {
            StopCoroutine(movementCoroutine);
            movementCoroutine = null;
        }
    }

    private IEnumerator StartFloorMovementWithDelay()
    {
        // Start each floor's movement with a delay
        for (int i = 0; i < floor.Length; i++)
        {
            StartCoroutine(MoveFloor(i));  // Start the coroutine for each floor
            yield return new WaitForSeconds(startDelay);  // Add delay between starting each floor
        }
    }

    private IEnumerator MoveFloor(int index)
    {
        while (true)  // Infinite loop for continuous movement
        {
            // Move up or down based on the direction
            if (movingUp[index])
            {
                // Move up
                floor[index].transform.position += Vector3.up * moveSpeed * Time.deltaTime;

                // If it reaches the max distance, reverse direction
                if (floor[index].transform.position.y >= initialPositions[index].y + moveDistance)
                {
                    movingUp[index] = false;  // Start moving down
                }
            }
            else
            {
                // Move down
                floor[index].transform.position -= Vector3.up * moveSpeed * Time.deltaTime;

                // If it reaches the starting position, reverse direction
                if (floor[index].transform.position.y <= initialPositions[index].y)
                {
                    movingUp[index] = true;  // Start moving up
                }
            }

            // Wait until the next frame
            yield return null;
        }
    }

    protected override void Update()
    {
    }
}
