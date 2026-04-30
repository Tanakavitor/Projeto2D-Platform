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
    private Vector2 moveInput;
    private Vector2 lastMoveDirection = Vector2.down;

    private bool isDashing = false;
    private float dashTimer = 0f;
    private float dashCooldownTimer = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Leitura do input
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        // Guarda a ultima direcao para o dash
        if (moveInput != Vector2.zero)
            lastMoveDirection = moveInput;

        // Cooldown do dash
        if (dashCooldownTimer > 0)
            dashCooldownTimer -= Time.deltaTime;

        // Ativar dash com Shift
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && dashCooldownTimer <= 0)
            StartDash();

        // Timer do dash
        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
                isDashing = false;
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
            rb.linearVelocity = lastMoveDirection * dashSpeed;
        else
            rb.linearVelocity = moveInput * moveSpeed;
    }

    void StartDash()
    {
        isDashing = true;
        dashTimer = dashDuration;
        dashCooldownTimer = dashCooldown;
    }
}