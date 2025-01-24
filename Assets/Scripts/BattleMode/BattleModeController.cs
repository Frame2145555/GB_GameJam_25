using UnityEngine;

public class BattleModeController : MonoBehaviour
{
    public enum m_mode { sideScroll, topdown }
    public m_mode Mode;
    public float m_topdownSpeed = 5.0f;
    public float m_sideScrollSpeed = 5.0f;
    public float m_sideJumpHeight = 15.0f;

    private Rigidbody2D rb;
    public Vector2 moveDirection;
    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundlayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ChangeMode();
        Ability();
        Move();
        Jump();
    }

    public void ChangeMode()
    {
        switch (Mode)
        {
            case m_mode.sideScroll:
                HandleSideScroll();
                break;

            case m_mode.topdown:
                HandleTopdown();
                break;
        }
    }
    public void HandleSideScroll()
    {
        float m_Input = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(m_Input * m_sideScrollSpeed, rb.linearVelocity.y);

    }
    public void HandleTopdown()
    {
        float moveInputX = Input.GetAxisRaw("Horizontal");
        float moveInputY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveInputX, moveInputY).normalized;
        rb.linearVelocity = moveDirection * m_topdownSpeed;
    }

    public void Move()
    {
        float m_Input = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(m_Input * m_sideScrollSpeed, rb.linearVelocity.y);
    }
    public void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, m_sideJumpHeight);
        }
    }
    public void Ability()
    {

    }
    public bool isGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundlayer))
        {
            Debug.Log("is grounded");
            return true; 
        }
        else
        {
            Debug.Log("is not grounded");
            return false; 
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }
}

