using UnityEngine;

public class D8PlayerControllerSample : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] float speed;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }
    void Update()
    {
        rb.linearVelocity = speed * InputManager.Instance.GetMoveVector();
    }
}
