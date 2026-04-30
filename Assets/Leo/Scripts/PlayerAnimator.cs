using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Atualiza o Speed baseado na velocidade do Rigidbody
        animator.SetFloat("Speed", rb.linearVelocity.magnitude);
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