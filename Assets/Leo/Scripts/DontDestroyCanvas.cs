using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyCanvas : MonoBehaviour
{
    private static DontDestroyCanvas instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Victory" || scene.name == "MainMenu" || 
            scene.name == "GameOver" || scene.name == "Instructions" || 
            scene.name == "IntroStory")
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            instance = null;
            Destroy(gameObject);
        }
    }
}