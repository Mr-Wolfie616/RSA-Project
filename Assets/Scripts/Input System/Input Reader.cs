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
   public bool Aim {get; private set;}
   public bool Fire {get; private set;}
   public bool Reload {get; private set;}

   private PlayerInput playerInput;

   private InputAction moveAction;
   private InputAction lookAction;
   private InputAction crouchAction;
   private InputAction sprintAction;
   private InputAction aimAction;
   private InputAction fireAction;
   private InputAction reloadAction;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        var gameplay = playerInput.actions.FindActionMap("Gameplay", true);

        moveAction = gameplay.FindAction("Move", true);
        lookAction = gameplay.FindAction("Look", true);
        crouchAction = gameplay.FindAction("Crouch", true);
        sprintAction = gameplay.FindAction("Sprint", true);
        aimAction = gameplay.FindAction("Aim", true);
        fireAction = gameplay.FindAction("Fire", true);
        reloadAction = gameplay.FindAction("Reload", true);
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

        aimAction.performed += OnAim;
        aimAction.canceled += OnAim;

        fireAction.performed += OnFire;
        fireAction.canceled += OnFire;

        reloadAction.performed += OnReload;
        reloadAction.canceled += OnReload;
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

        aimAction.performed -= OnAim;
        aimAction.canceled -= OnAim;

        fireAction.performed -= OnFire;
        fireAction.canceled -= OnFire;

        reloadAction.performed -= OnReload;
        reloadAction.canceled -= OnReload;
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
        Fire = ctx.ReadValueAsButton();
    }

    public void clear()
    {
        Move = Vector2.zero;
        Look = Vector2.zero;
        Crouch = false;
        Sprint = false;
        Aim = false;
        Fire = false;
        Reload = false;
    }

    private void OnGUI()
    {
        var cam = playerInput.camera;
        if(cam == null) cam = GetComponentInChildren<Camera>(true);
        if(cam == null) cam = Camera.main;
        if(cam == null) return;

        Rect pr = cam.pixelRect;

        float pad = 10f;
        float w = 320f;
        float h = 120f;

        var area = new Rect(pr.xMin + pad, pr.yMin + pad, w, h);

        GUILayout.BeginArea(area, GUI.skin.box);
        GUILayout.Label($"{playerInput.playerIndex} Scheme: {playerInput.currentControlScheme}");

        GUILayout.Label($"Move: {Move}");
        GUILayout.Label($"Look: {Look}");
        GUILayout.Label($"Crouch: {Crouch}");
        GUILayout.Label($"Sprint: {Sprint}");
        GUILayout.Label($"Aim: {Aim}");
        GUILayout.Label($"Fire: {Fire}");
        GUILayout.EndArea();
    }
}
