using UnityEngine;
public enum ControllerMode 
{ 
    SideScroll, 
    Topdown 
}

public class BattleModeController : MonoBehaviour
{
    public ControllerMode Mode;
    public float m_topdownSpeed = 5.0f;
    public float m_sideScrollSpeed = 5.0f;
    public float m_sideJumpHeight = 15.0f;

    private Rigidbody2D rb;
    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundlayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
    }
    public void Move()
    {
        switch (Mode)
        {
            case ControllerMode.SideScroll:
                HandleSideScroll();
                Jump();
                break;
            case ControllerMode.Topdown:
                HandleTopdown();
                break;
        }
    }

    public void ChangeMode(ControllerMode mode)
    {
        Mode = mode;
    }
    public void HandleSideScroll()
    {
        float m_Input = InputManager.Instance.GetMoveVector().x;
        rb.linearVelocity = new Vector2(m_Input * m_sideScrollSpeed, rb.linearVelocity.y);
    }

    public void HandleTopdown()
    {
        Vector2 moveDirection = InputManager.Instance.GetMoveVector();
        rb.linearVelocity = moveDirection * m_topdownSpeed;
    }

    public void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded())
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, m_sideJumpHeight);
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

