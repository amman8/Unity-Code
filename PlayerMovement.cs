using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float crouchSpeed = 6f;
    public float sprintSpeed = 18f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float sprintDuration = 5f; // Duration of sprint in seconds
    public float sprintCooldown = 5f; // Cooldown duration after sprint in seconds

    private Vector3 velocity;
    private bool isGrounded;
    private bool isCrouching = false;
    private float originalHeight;
    public float crouchHeight = 1f;

    private float sprintTimeLeft;
    private float sprintCooldownTimeLeft;
    private bool canSprint = true;

    void Start()
    {
        originalHeight = controller.height;
        sprintTimeLeft = sprintDuration;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        float currentSpeed = speed;

        if (Input.GetButtonDown("Crouch"))
        {
            Crouch();
        }

        if (Input.GetButtonUp("Crouch"))
        {
            StandUp();
        }

        if (Input.GetButtonDown("Sprint") && canSprint)
        {
            currentSpeed = sprintSpeed;
        }

        if (Input.GetButton("Sprint") && canSprint)
        {
            currentSpeed = sprintSpeed;
            sprintTimeLeft -= Time.deltaTime;

            if (sprintTimeLeft <= 0)
            {
                canSprint = false;
                sprintCooldownTimeLeft = sprintCooldown;
            }
        }
        else if (sprintTimeLeft < sprintDuration && !canSprint)
        {
            sprintCooldownTimeLeft -= Time.deltaTime;

            if (sprintCooldownTimeLeft <= 0)
            {
                canSprint = true;
                sprintTimeLeft = sprintDuration;
            }
        }

        if (isCrouching)
        {
            currentSpeed = crouchSpeed;
        }

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * currentSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void Crouch()
    {
        controller.height = crouchHeight;
        isCrouching = true;
    }

    void StandUp()
    {
        controller.height = originalHeight;
        isCrouching = false;
    }
}
