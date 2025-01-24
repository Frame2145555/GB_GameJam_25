using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerSample : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 5;
    [SerializeField] float jumpStrength = 5;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // stop movement if dialouge is playing
        if (DialogueManager.Instance.DialogueIsPlaying) return;

       rb.linearVelocityX = moveSpeed * InputManager.Instance.GetMoveVector().x;

        if (InputManager.Instance.IsJumpKeyDown)
        {
            rb.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
        }


    }
}
