using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputReader : MonoBehaviour
{
   public Vector2 Move {get; private set;}
   public Vector2 Look {get; private set;}

   public bool Crouch {get; private set;}
   public bool Sprint {get; private set;}
   public bool Jump {get; private set;}
   public bool Aim {get; private set;}
   public bool Fire {get; private set;}
   public bool Reload {get; private set;}
   public bool pausePressed {get; private set;}

   private PlayerInput playerInput;

   private InputAction moveAction;
   private InputAction lookAction;
   private InputAction crouchAction;
   private InputAction sprintAction;
   private InputAction jumpAction;
   private InputAction aimAction;
   private InputAction fireAction;
   private InputAction reloadAction;
   private InputAction pauseAction;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        var gameplay = playerInput.actions.FindActionMap("Gameplay", true);

        moveAction = gameplay.FindAction("Move", true);
        lookAction = gameplay.FindAction("Look", true);
        crouchAction = gameplay.FindAction("Crouch", true);
        sprintAction = gameplay.FindAction("Sprint", true);
        jumpAction = gameplay.FindAction("Jump", true);
        aimAction = gameplay.FindAction("Aim", true);
        fireAction = gameplay.FindAction("Fire", true);
        reloadAction = gameplay.FindAction("Reload", true);
        pauseAction = gameplay.FindAction("Pause", true);
    }

    private void OnEnable()
    {
        playerInput.actions.FindActionMap("Gameplay", true).Enable();

        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;

        lookAction.performed += OnLook;
        lookAction.canceled += OnLook;

        crouchAction.performed += OnCrouch;
        crouchAction.canceled += OnCrouch;

        sprintAction.performed += OnSprint;
        sprintAction.canceled += OnSprint;

        jumpAction.performed += OnJump;
        jumpAction.canceled += OnJump;

        aimAction.performed += OnAim;
        aimAction.canceled += OnAim;

        fireAction.performed += OnFire;
        fireAction.canceled += OnFire;

        reloadAction.performed += OnReload;
        reloadAction.canceled += OnReload;

        pauseAction.performed += OnPause;
    }

    private void OnDisable()
    {
        moveAction.performed -= OnMove;
        moveAction.canceled -= OnMove;

        lookAction.performed -= OnLook;
        lookAction.canceled -= OnLook;

        crouchAction.performed -= OnCrouch;
        crouchAction.canceled -= OnCrouch;

        sprintAction.performed -= OnSprint;
        sprintAction.canceled -= OnSprint;

        jumpAction.performed -= OnJump;
        jumpAction.canceled -= OnJump;

        aimAction.performed -= OnAim;
        aimAction.canceled -= OnAim;

        fireAction.performed -= OnFire;
        fireAction.canceled -= OnFire;

        reloadAction.performed -= OnReload;
        reloadAction.canceled -= OnReload;

        pauseAction.performed -= OnPause;
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        Move = ctx.ReadValue<Vector2>();
    }

    private void OnLook(InputAction.CallbackContext ctx)
    {
        Look = ctx.ReadValue<Vector2>();
    }

    private void OnCrouch(InputAction.CallbackContext ctx)
    {
        Crouch = ctx.ReadValueAsButton();
    }

    private void OnSprint(InputAction.CallbackContext ctx)
    {
        Sprint = ctx.ReadValueAsButton();
    }

    private void OnJump(InputAction.CallbackContext ctx)
    {
        Jump = ctx.ReadValueAsButton();
    }

    private void OnAim(InputAction.CallbackContext ctx)
    {
        Aim = ctx.ReadValueAsButton();
    }

    private void OnFire(InputAction.CallbackContext ctx)
    {
        Fire = ctx.ReadValueAsButton();
    }

     private void OnReload(InputAction.CallbackContext ctx)
    {
        Reload = ctx.ReadValueAsButton();
    }

    private void OnPause(InputAction.CallbackContext ctx)
    {
        pausePressed = true;
    }


    public void clear()
    {
        Move = Vector2.zero;
        Look = Vector2.zero;
        Crouch = false;
        Sprint = false;
        Jump = false;
        Aim = false;
        Fire = false;
        Reload = false;
    }
}
