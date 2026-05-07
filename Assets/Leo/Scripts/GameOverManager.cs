using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("Leo/Scenes/SampleScene");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}