using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Ataque")]
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    public float attackCooldown = 0.5f;

    private float attackCooldownTimer = 0f;
    private PlayerAnimator playerAnimator;

    void Awake()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
    }

    void Update()
    {
        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;

            // Reseta a animacao de ataque na metade do cooldown
            if (attackCooldownTimer <= attackCooldown / 2f)
                playerAnimator.SetAttacking(false);
        }

        if (Input.GetMouseButtonDown(0) && attackCooldownTimer <= 0)
            Shoot();
    }

    void Shoot()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - transform.position).normalized;

        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().linearVelocity = direction * projectileSpeed;

        playerAnimator.SetAttacking(true);
        attackCooldownTimer = attackCooldown;
    }
}