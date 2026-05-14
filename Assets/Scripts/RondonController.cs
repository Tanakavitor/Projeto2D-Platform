using UnityEngine;

public class RondonController : MonoBehaviour
{
    [Header("Stats")]
    public float speed = 2f;
    public int maxHP = 20;
    public float attackRange = 1f;
    public int attackDamage = 1;
    public float attackCooldown = 1.5f;

    [Header("Fases")]
    public float phase2HPThreshold = 0.6f;
    public float phase3HPThreshold = 0.3f;
    public float phase2SpeedMultiplier = 1.5f;
    public float phase3SpeedMultiplier = 2f;

    private int currentHP;
    private int currentPhase = 1;
    private Transform player;
    private Animator animator;
    private Rigidbody2D rb;
    private float attackTimer;
    private bool isDead = false;
    private Vector2 moveDirection;
    private Region3Spawner spawner;

    void Start()
    {
        currentHP = maxHP;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;

        spawner = FindObjectOfType<Region3Spawner>();
    }

    void Update()
    {
        if (isDead || player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        attackTimer -= Time.deltaTime;

        float currentSpeed = speed;
        if (currentPhase == 2) currentSpeed *= phase2SpeedMultiplier;
        if (currentPhase == 3) currentSpeed *= phase3SpeedMultiplier;

        if (distanceToPlayer <= attackRange)
        {
            moveDirection = Vector2.zero;
            rb.linearVelocity = Vector2.zero;
            animator.SetFloat("Speed", 0f);
            animator.SetBool("IsAttacking", true);

            if (attackTimer <= 0f)
            {
                attackTimer = attackCooldown;
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                    playerHealth.TakeDamage(attackDamage);
            }
        }
        else
        {
            moveDirection = (player.position - transform.position).normalized;
            rb.linearVelocity = moveDirection * currentSpeed;

            animator.SetFloat("Speed", moveDirection.magnitude);
            animator.SetFloat("MoveX", moveDirection.x);
            animator.SetFloat("MoveY", moveDirection.y);
            animator.SetBool("IsAttacking", false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
                playerHealth.TakeDamage(attackDamage);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;
        currentHP -= damage;
        animator.SetBool("IsHurt", true);
        StartCoroutine(ResetHurt());

        float hpPercent = (float)currentHP / maxHP;
        if (hpPercent <= phase3HPThreshold && currentPhase < 3)
        {
            currentPhase = 3;
            attackCooldown *= 0.7f;
        }
        else if (hpPercent <= phase2HPThreshold && currentPhase < 2)
        {
            currentPhase = 2;
            attackCooldown *= 0.85f;
        }

        if (currentHP <= 0)
            Die();
    }

    System.Collections.IEnumerator ResetHurt()
    {
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("IsHurt", false);
    }

    void Die()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;
        animator.SetBool("IsDead", true);

        if (spawner != null)
            spawner.OnBossDied();

        Destroy(gameObject, 2f);
    }
}