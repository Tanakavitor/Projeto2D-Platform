using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Vida")]
    public int maxHearts = 3;
    private int currentHearts;

    private PlayerAnimator playerAnimator;
    private bool isDead = false;

    void Awake()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
        currentHearts = maxHearts;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHearts -= damage;

        if (currentHearts <= 0)
        {
            currentHearts = 0;
            Die();
        }
        else
        {
            StartCoroutine(HurtRoutine());
        }
    }

    System.Collections.IEnumerator HurtRoutine()
    {
        playerAnimator.SetHurt(true);
        yield return new WaitForSeconds(0.3f);
        playerAnimator.SetHurt(false);
    }

    void Die()
    {
        isDead = true;
        playerAnimator.SetDead(true);
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerAttack>().enabled = false;
        StartCoroutine(GameOverRoutine());
    }

    System.Collections.IEnumerator GameOverRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("GameOver");
    }

    public int GetCurrentHearts()
    {
        return currentHearts;
    }
    
}