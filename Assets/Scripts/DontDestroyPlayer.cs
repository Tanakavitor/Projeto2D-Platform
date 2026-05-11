using UnityEngine;

public class DontDestroyPlayer : MonoBehaviour
{
    private static DontDestroyPlayer instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("Player criado e persistindo");
        }
        else
        {
            Debug.Log("Duplicata destruida");
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        Debug.Log("Player destruido!");
    }
}