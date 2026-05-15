using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroStoryManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadScene("Regiao1");
    }
}