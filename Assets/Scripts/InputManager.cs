using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    static InputManager instance;

    InputAction move;
    InputAction jump;
    InputAction interact;

    bool isJumpKeyDown;
    bool isJumpKeyPressed;
    bool hasJumpKeyPress;
    bool isJumpKeyUp;

    bool isInteractKeyDown;
    bool isInteractKeyPressed;
    bool hasInteractKeyPress;
    bool isInteractKeyUp;

    static public InputManager Instance { get => instance; private set => instance = value; }
    public bool IsJumpKeyDown       { get => isJumpKeyDown; }
    public bool IsJumpKeyPressed    { get => isJumpKeyPressed; }
    public bool IsJumpKeyUp         { get => isJumpKeyUp; }
    public bool IsInteractKeyDown   { get => isInteractKeyDown; }
    public bool IsInteractKeyPressed{ get => isInteractKeyPressed; }
    public bool IsInteractKeyUp     { get => isInteractKeyUp; }
    public Vector2 GetMoveVector() => move.ReadValue<Vector2>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    private void Start()
    {
        move = InputSystem.actions.FindAction("Move");
        jump = InputSystem.actions.FindAction("Jump");
        interact = InputSystem.actions.FindAction("Interact");
    }

    private void Update()
    {
        isJumpKeyPressed = jump.IsPressed();
        if (isJumpKeyPressed)
        {
            if (!hasJumpKeyPress)
            {
                isJumpKeyDown = true;
                hasJumpKeyPress = true;
            }
            else
            {
                isJumpKeyDown = false;
            }
        }
        else
        {
            if (hasJumpKeyPress)
            {
                isJumpKeyUp = true;
                hasJumpKeyPress = false;
            }
            else
            {
                isJumpKeyUp = false;
            }

        }

        isInteractKeyPressed = interact.IsPressed();
        if (isInteractKeyPressed)
        {
            if (!hasInteractKeyPress)
            {
                isInteractKeyDown = true;
                hasInteractKeyPress = true;
            }
            else
            {
                isInteractKeyDown = false;
            }
        }
        else
        {
            if (hasInteractKeyPress)
            {
                isInteractKeyUp = true;
                hasInteractKeyPress = false;
            }
            else
            {
                isInteractKeyUp = false;
            }
        }
    }


}
