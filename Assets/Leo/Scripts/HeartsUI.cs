using UnityEngine;
using UnityEngine.UI;

public class HeartsUI : MonoBehaviour
{
    public Image heart1;
    public Image heart2;
    public Image heart3;

    public Sprite heartFull;
    public Sprite heartEmpty;

    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    void Update()
    {
        int hearts = playerHealth.GetCurrentHearts();

        heart1.sprite = hearts >= 1 ? heartFull : heartEmpty;
        heart2.sprite = hearts >= 2 ? heartFull : heartEmpty;
        heart3.sprite = hearts >= 3 ? heartFull : heartEmpty;
    }
}