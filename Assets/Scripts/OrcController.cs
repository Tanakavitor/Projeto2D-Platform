using UnityEngine;

public class OrcController : MonoBehaviour
{
    [Header("Stats")]
    public float speed = 3f;
    public int maxHP = 5;
    public float attackRange = 0.8f;
    public int attackDamage = 1;
    public float attackCooldown = 1.5f;

    private int currentHP;
    private Transform player;
    private Animator animator;
    private Rigidbody2D rb;
    private float attackTimer;
    private bool isDead = false;
    private Vector2 moveDirection;
    private OrcSpawner spawner;

    void Start()
    {
        currentHP = maxHP;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    void Update()
    {
        if (isDead || player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        attackTimer -= Time.deltaTime;

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
            rb.linearVelocity = moveDirection * speed;

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

    public void SetSpawner(OrcSpawner s)
    {
        spawner = s;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;
        currentHP -= damage;
        animator.SetBool("IsHurt", true);
        StartCoroutine(ResetHurt());

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
            spawner.OnOrcDied();

        Destroy(gameObject, 1.5f);
    }
}