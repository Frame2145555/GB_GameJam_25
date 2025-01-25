using UnityEngine;

public class PlayModeController : MonoBehaviour
{
    Rigidbody2D rb;
    Fade fade;

    [Header("Speed Control")]
    [SerializeField] float speed = 10;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fade = FindAnyObjectByType<Fade>();
    }

    private void Update()
    {
        if (DialogueManager.Instance.DialogueIsPlaying || fade.IsFadingOut)
        {
            rb.linearVelocityX = 0;
            return;
        }

        float dir = InputManager.Instance.GetMoveVector().x;
        rb.linearVelocityX = dir * speed;
    }
}
