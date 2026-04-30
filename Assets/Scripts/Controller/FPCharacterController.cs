using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(InputReader))]
public class FPCharacterController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float backwardSpeed = 2.5f;
    Vector3 moveVelocity;
    private float speedDebug = 0f;

    [Header("Camera")]
    [SerializeField] private float lookSensitivity = 2f;
    [SerializeField] private Transform CameraTransform;
    [SerializeField] private float xRotation;

    [Header("Crouch")]
    [SerializeField] private float crouchSpeed = 2f;
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 crouchScale = new Vector3 (0.5f, 0.5f, 0.5f);

    [Header("Jump")]
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] Transform groundCheck;
    [SerializeField] private float groundDistance = 0.3f;
    [SerializeField] LayerMask groundLayer;

    Vector3 velocity;
    private bool isGrounded; 
    private Vector3 standardScale = new Vector3 (1f, 1f, 1f);
    private CharacterController controller;
    private InputReader input;

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

    private void Update()
    {
        isGrounded = controller.isGrounded;
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Jump();
        Moveplayer();
        playerRotation();

        velocity.y += gravity * Time.deltaTime;

        Vector3 finalMove = moveVelocity * Time.deltaTime + velocity * Time.deltaTime;
        controller.Move(finalMove);
        Debug.Log(velocity.y);
    }

    void Moveplayer()
    {
        float currentSpeed;

        if(input.Move.y < 0)
        {
            currentSpeed = backwardSpeed;
        }
        else if (input.Move.y > 0.1f && input.Sprint)
        {
            currentSpeed = sprintSpeed;
        }
        else if (input.Move.y > 0.1f && input.Crouch)
        {
            currentSpeed = crouchSpeed;
        }
        else
        {
            currentSpeed = moveSpeed;
        }

        if (input.Crouch)
        {
            transform.localScale = crouchScale;
        }
        else
        {
            transform.localScale = standardScale;
        }

        Vector3 Forward = CameraTransform.forward;
        Vector3 Right = CameraTransform.right;

        Forward.y = 0f;
        Right.y = 0f;

        Forward.Normalize();
        Right.Normalize();


        Vector3 moveDir = (Forward * input.Move.y + Right * input.Move.x).normalized;

        moveVelocity = moveDir * currentSpeed;
        speedDebug = controller.velocity.magnitude;
        //Debug.Log(speedDebug);
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

    public void Jump()
    {
        if(input.Jump && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}
