using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("IntroStory");
    }

    public void GoToTutorial()
    {
        SceneManager.LoadScene("Instructions");
    }
}