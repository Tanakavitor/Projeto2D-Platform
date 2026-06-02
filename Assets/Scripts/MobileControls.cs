using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MobileControls : MonoBehaviour
{
    public Button attackButton;
    public Button dashButton;

    private PlayerAttack playerAttack;
    private PlayerMovement playerMovement;

    void Start()
    {
        playerAttack = FindObjectOfType<PlayerAttack>();
        playerMovement = FindObjectOfType<PlayerMovement>();

        attackButton.onClick.AddListener(OnAttackPressed);
        dashButton.onClick.AddListener(OnDashPressed);
    }

    void OnAttackPressed()
    {
        if (playerAttack != null)
            playerAttack.MobileShoot();
    }

    void OnDashPressed()
    {
        if (playerMovement != null)
            playerMovement.StartDash();
    }
}