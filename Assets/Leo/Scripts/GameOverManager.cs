using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("Regiao1");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}