using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryManager : MonoBehaviour
{
    public void GoToMenu()
    {
        // Destroi o player persistente antes de ir ao menu
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            Destroy(player);
            
        SceneManager.LoadScene("MainMenu");
    }
}