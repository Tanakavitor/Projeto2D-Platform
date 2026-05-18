using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyPlayer : MonoBehaviour
{
    private static DontDestroyPlayer instance;

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
        if (scene.name == "MainMenu" || scene.name == "Instructions" ||
            scene.name == "GameOver" || scene.name == "Victory" ||
            scene.name == "IntroStory")
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            instance = null;
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}