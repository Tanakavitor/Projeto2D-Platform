using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    public bool isResumeButton = false;
    public bool isMenuButton = false;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        Debug.Log("Botao clicado!");
        if (isResumeButton)
            PauseManager.Instance.Resume();
        else if (isMenuButton)
            PauseManager.Instance.GoToMenu();
    }
}