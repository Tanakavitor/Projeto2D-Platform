using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Vida")]
    public int maxHearts = 3;
    private int currentHearts;

    [Header("Invencibilidade")]
    public float invincibilityDuration = 1.5f;
    private bool isInvincible = false;

    private PlayerAnimator playerAnimator;
    private bool isDead = false;

    void Awake()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
        currentHearts = maxHearts;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Regiao1")
        {
            currentHearts = maxHearts;
            isDead = false;
            isInvincible = false;
            playerAnimator.SetHurt(false);
            playerAnimator.SetDead(false);
            GetComponent<PlayerMovement>().enabled = true;
            GetComponent<PlayerAttack>().enabled = true;
        }

        if (scene.name == "GameOver" || scene.name == "MainMenu")
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void TakeDamage(int damage)
    {
        if (isDead || isInvincible) return;
        
        PlayerMovement movement = GetComponent<PlayerMovement>();
        if (movement != null && movement.IsDashing) return;

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
        isInvincible = true;
        playerAnimator.SetHurt(true);
        yield return new WaitForSeconds(0.3f);
        playerAnimator.SetHurt(false);
        yield return new WaitForSeconds(invincibilityDuration - 0.3f);
        isInvincible = false;
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