using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Regiao1");
    }

    public void GoToTutorial()
    {
        SceneManager.LoadScene("Instructions");
    }
}