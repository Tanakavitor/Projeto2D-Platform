using UnityEngine;

public class SlimeController : MonoBehaviour
{
    [Header("Stats")]
    public float speed = 2f;
    public int maxHP = 3;
    public float detectionRange = 100f;
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
            animator.SetBool("isAttacking", true);

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
            animator.SetFloat("DirX", moveDirection.x);
            animator.SetFloat("DirY", moveDirection.y);
            animator.SetBool("isAttacking", false);
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
        animator.SetTrigger("isHurt");

        if (currentHP <= 0)
            Die();
    }

    private SlimeSpawner spawner;
    
    public void SetSpawner(SlimeSpawner s)
    {
        spawner = s;
    }
    
    void Die()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;
        animator.SetTrigger("isDead");
    
        if (spawner != null)
            spawner.OnSlimeDied();
    
        Destroy(gameObject, 1.5f);
    }
}