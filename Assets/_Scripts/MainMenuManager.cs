using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuManager : MonoBehaviour
{   
    [SerializeField]
    private GameObject _help;


    void Update()
    {
        bool helpPressed = Input.GetKeyDown(KeyCode.H);
        bool returnPressed = Input.GetKeyDown(KeyCode.Return);
        bool escPressed = Input.GetKeyDown(KeyCode.Escape);
        bool anyPressed = helpPressed || returnPressed || escPressed;

        if (_help.activeSelf && anyPressed) {
            HideHelp();
        } else {
            if (helpPressed) {
                ShowHelp();
            }

            if (returnPressed) {
                StartGame();
            }

            if (escPressed) {
                Application.Quit();
            }
        }
    }

    void StartGame() {
        SceneManager.LoadScene("Main");
    }

    void ShowHelp() {
        _help.SetActive(true);
    }

    void HideHelp() {
        _help.SetActive(false);
    }
}
