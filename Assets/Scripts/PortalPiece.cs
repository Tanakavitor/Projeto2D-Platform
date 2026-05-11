using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalPiece : MonoBehaviour
{
    public string nextScene;
    public bool isFinal = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (isFinal)
                Destroy(other.gameObject);

            SceneManager.LoadScene(nextScene);
        }
    }
}