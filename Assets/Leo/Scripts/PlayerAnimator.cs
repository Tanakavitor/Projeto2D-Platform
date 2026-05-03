using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 lastDirection = Vector2.down;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 velocity = rb.linearVelocity;
        float speed = velocity.magnitude;
        animator.SetFloat("Speed", speed);

        if (speed > 0.1f)
        {
            lastDirection = velocity.normalized;
            animator.SetFloat("MoveX", lastDirection.x);
            animator.SetFloat("MoveY", lastDirection.y);
        }
    }

    public void SetAttacking(bool value)
    {
        animator.SetBool("IsAttacking", value);
    }

    public void SetDashing(bool value)
    {
        animator.SetBool("IsDashing", value);
    }

    public void SetHurt(bool value)
    {
        animator.SetBool("IsHurt", value);
    }

    public void SetDead(bool value)
    {
        animator.SetBool("IsDead", value);
    }
}