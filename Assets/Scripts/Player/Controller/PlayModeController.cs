using UnityEngine;

public class PlayModeController : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("Speed Control")]
    [SerializeField] float speed = 10;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (DialogueManager.Instance.DialogueIsPlaying)
        {
            rb.linearVelocityX = 0;
            return;
        }

        float dir = InputManager.Instance.GetMoveVector().x;
        rb.linearVelocityX = dir * speed;
    }
}
