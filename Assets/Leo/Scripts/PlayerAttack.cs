using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Ataque")]
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    public float attackCooldown = 0.5f;

    [Header("Audio")]
    public AudioClip attackSound;
    private AudioSource audioSource;

    private float attackCooldownTimer = 0f;
    private PlayerAnimator playerAnimator;

    void Awake()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
            if (attackCooldownTimer <= attackCooldown / 2f)
                playerAnimator.SetAttacking(false);
        }

        if (Time.timeScale == 0f) return;

        if (Input.GetMouseButtonDown(0) && attackCooldownTimer <= 0)
            Shoot();
    }

    void Shoot()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - transform.position).normalized;

        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().linearVelocity = direction * projectileSpeed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(0, 0, angle);

        if (audioSource != null && attackSound != null)
            audioSource.PlayOneShot(attackSound);

        playerAnimator.SetAttacking(true);
        attackCooldownTimer = attackCooldown;
    }
}