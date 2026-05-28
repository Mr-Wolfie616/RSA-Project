using UnityEngine;


[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(InputReader))]
public class FPCharacterController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float backwardSpeed = 2.5f;
    
    [Header("Camera")]
    [SerializeField] private float lookSensitivity = 5f;
    [SerializeField] private Transform CameraTransform;
    [SerializeField] private float xRotation;

    [Header("Crouch")]
    [SerializeField] private float crouchSpeed = 2f;
    [SerializeField] private Vector3 crouchScale = new Vector3 (0.5f, 0.5f, 0.5f);
    private Vector3 standardScale = Vector3.one;

    [Header("Jump")]
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.3f;
    [SerializeField] private LayerMask groundLayer;

    Vector3 velocity;
    Vector3 moveVelocity;
    private bool isGrounded; 

    private CharacterController controller;
    private InputReader input;

    private Playerstate currentState;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        input = GetComponent<InputReader>();
        if (CameraTransform == null)
        {
            var cam = GetComponentInChildren<Camera>();
            if (cam != null) CameraTransform = cam.transform;
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start()
    {
        ChangeState(new IdleState(this));
    }

    private void Update()
    {
        GroundCheck();
        Jump();
        playerRotation();

        currentState.Update();

        Gravity();
        Move();
    }

    public void ChangeState(Playerstate newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    private void GroundCheck()
    {
         isGrounded = Physics.Raycast(groundCheck.position,Vector3.down,groundDistance,groundLayer);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

     public void Jump()
    {
        if(input.Jump && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            ChangeState(new JumpState(this));
        }
    }

    private void Gravity()
    {
         velocity.y += gravity * Time.deltaTime;
    }

    private void Move()
    {
        Vector3 finalMove = moveVelocity * Time.deltaTime + velocity * Time.deltaTime;
        controller.Move(finalMove);
    }

     public void playerRotation()
    {
        float mouseY = input.Look.y * lookSensitivity * Time.deltaTime;
        float mouseX = input.Look.x * lookSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        CameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    public void SetMovement(float speed)
    {
        Vector3 Forward = CameraTransform.forward;
        Vector3 Right = CameraTransform.right;

        Forward.y = 0f;
        Right.y = 0f;

        Forward.Normalize();
        Right.Normalize();


        Vector3 moveDir = (Forward * input.Move.y + Right * input.Move.x).normalized;

        moveVelocity = moveDir * speed;
    }

    public void SetStanding()
    {
        transform.localScale = standardScale;
    }

    public void SetCrouching()
    {
        transform.localScale = crouchScale;
    }

    public bool IsGrounded => isGrounded;
    public InputReader Input => input;

    public float MoveSpeed => moveSpeed;
    public float SprintSpeed => sprintSpeed;
    public float BackwardSpeed => backwardSpeed;
    public float CrouchSpeed => crouchSpeed;
}
