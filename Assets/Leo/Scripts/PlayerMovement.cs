using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimento")]
    public float moveSpeed = 5f;

    [Header("Dash")]
    public float dashSpeed = 15f;
    public float dashDuration = 0.15f;
    public float dashCooldown = 1f;

    private Rigidbody2D rb;
    private PlayerAnimator playerAnimator;
    private Vector2 moveInput;
    private Vector2 lastMoveDirection = Vector2.down;

    private bool isDashing = false;
    public Vector2 LastMoveDirection => lastMoveDirection;
    private float dashTimer = 0f;
    private float dashCooldownTimer = 0f;

    private VirtualJoystick joystick;

    public bool IsDashing => isDashing;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<PlayerAnimator>();
        joystick = FindObjectOfType<VirtualJoystick>();
    }

    void Update()
    {
        // Input do teclado
        Vector2 keyboardInput = Vector2.zero;
        keyboardInput.x = Input.GetAxisRaw("Horizontal");
        keyboardInput.y = Input.GetAxisRaw("Vertical");
        keyboardInput.Normalize();

        // Input do joystick virtual
        Vector2 joystickInput = Vector2.zero;
        if (joystick != null)
            joystickInput = joystick.InputDirection;

        // Usa o que tiver sendo usado
        if (joystickInput.magnitude > 0.1f)
            moveInput = joystickInput;
        else
            moveInput = keyboardInput;

        if (moveInput != Vector2.zero)
            lastMoveDirection = moveInput;

        if (dashCooldownTimer > 0)
            dashCooldownTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && dashCooldownTimer <= 0)
            StartDash();

        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                isDashing = false;
                playerAnimator.SetDashing(false);
            }
        }
    }
    void FixedUpdate()
    {
        if (isDashing)
            rb.linearVelocity = lastMoveDirection * dashSpeed;
        else
            rb.linearVelocity = moveInput * moveSpeed;
    }

    public void StartDash()
    {
        if (isDashing || dashCooldownTimer > 0) return;
        isDashing = true;
        dashTimer = dashDuration;
        dashCooldownTimer = dashCooldown;
        playerAnimator.SetDashing(true);
    }
}