using UnityEngine;
using UnityEngine.UI;

public class HeartsUI : MonoBehaviour
{
    public Image heart1;
    public Image heart2;
    public Image heart3;

    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    void Update()
    {
        int hearts = playerHealth.GetCurrentHearts();

        heart1.enabled = hearts >= 1;
        heart2.enabled = hearts >= 2;
        heart3.enabled = hearts >= 3;
    }
}