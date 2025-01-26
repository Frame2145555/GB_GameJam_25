using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed = 5f;            // Speed of the bullet
    private Transform player;           // Reference to the player's position
    private Rigidbody2D rb;             // Reference to the Rigidbody2D component

    void Start()
    {
        // Find the player in the scene (assuming the player object is tagged as "Player")
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();

        // If there's a player and Rigidbody2D, start moving the bullet
        if (player != null && rb != null)
        {
            // Calculate direction toward the player
            Vector2 direction = (player.position - transform.position).normalized;

            // Apply force to move the bullet toward the player
            rb.AddForce(direction * speed, ForceMode2D.Impulse); // Use Impulse to apply an immediate force
        }
    }

    void Update()
    {
        if (player != null)
        {
            // Optionally, if you'd like the bullet to rotate toward the player using LookAt
            // Note: You could also make the bullet rotate smoothly with Mathf.LerpAngle for a smoother effect.
            Vector2 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;  // Set rotation to face the player
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Handle collision with the player or anything else
        if (collision.gameObject.CompareTag("Player"))
        {
            // Handle damage or any interaction with the player here
            Destroy(gameObject);  // Destroy the bullet on impact
        }
        else
        {
            Destroy(gameObject);  // Destroy the bullet when it collides with anything else
        }
    }
}