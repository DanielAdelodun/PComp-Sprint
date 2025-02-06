using UnityEngine;

public class InputManager : MonoBehaviour
{
    InputSystem playerControls;
    
    public Vector2 lookInput;
    public Vector2 mousePosition;
    public Vector2 moveInput;
    private bool jumpPressed;
    public bool JumpPressed 
    { 
        get 
        { 
            bool jump = jumpPressed;
            jumpPressed = false; // Reset after read
            return jump;
        } 
    }

    private PlayerManager player;

    void OnEnable()
    {
        player = GetComponent<PlayerManager>();
        if (playerControls == null)
        {
            playerControls = new InputSystem();
            playerControls.Player.Look.performed += ctx => HandleLookInput();
            playerControls.Player.Move.performed += ctx => HandleMoveInput();
            playerControls.Player.Jump.performed += ctx => HandleJumpInput();
            playerControls.Player.Launch.performed += ctx => HandleLaunchInput();
            playerControls.Player.Point.performed += ctx => HandlePointerInput();
        }
        playerControls.Enable();
    }

    private void HandlePointerInput()
    {
        mousePosition = playerControls.Player.Point.ReadValue<Vector2>();
    }

    private void HandleJumpInput()
    {
        jumpPressed = true;
    }

    private void HandleLaunchInput()
    {
        player.OnLaunch();
    }

    private void HandleLookInput()
    {
        lookInput = playerControls.Player.Look.ReadValue<Vector2>();
    }

    private void HandleMoveInput()
    {
        moveInput = playerControls.Player.Move.ReadValue<Vector2>();
    }

    void OnDisable()
    {
        playerControls.Disable();
    }
}
