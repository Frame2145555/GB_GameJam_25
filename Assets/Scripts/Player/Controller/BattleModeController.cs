using UnityEngine;
public enum ControllerMode 
{ 
    SideScroll, 
    Topdown 
}

public class BattleModeController : MonoBehaviour
{
    public ControllerMode Mode = ControllerMode.Topdown;
    public float m_topdownSpeed = 5.0f;
    public float m_sideScrollSpeed = 5.0f;
    public float m_sideJumpHeight = 15.0f;

    [SerializeField] float localGravity = 3;

    private Rigidbody2D rb;
    public float castDistance;
    public LayerMask groundlayer;

    bool isControllerActive = false;

    void ActivateController() => isControllerActive = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        BattleModeManager.Instance.OnBattleModeEnter += ActivateController;

        rb.gravityScale = 0;
    }

    private void OnDisable()
    {
        BattleModeManager.Instance.OnBattleModeEnter -= ActivateController;
    }

    void Update()
    {
        if (!isControllerActive)
            return;

        if (DialogueManager.Instance.DialogueIsPlaying)
            return;


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
        rb.gravityScale = localGravity;

        float m_Input = InputManager.Instance.GetMoveVector().x;
        rb.linearVelocity = new Vector2(m_Input * m_sideScrollSpeed, rb.linearVelocity.y);
    }

    public void HandleTopdown()
    {
        rb.gravityScale = 0;

        Vector2 moveDirection = InputManager.Instance.GetMoveVector();
        rb.linearVelocity = moveDirection * m_topdownSpeed;
    }

    public void Jump()
    {
        if (InputManager.Instance.IsJumpKeyDown && isGrounded())
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, m_sideJumpHeight);
    }
    public void Ability()
    {

    }
    public bool isGrounded()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, castDistance,groundlayer))
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
        Gizmos.DrawLine(transform.position, transform.position - transform.up * castDistance);
    }
}

