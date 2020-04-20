using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{   
    [SerializeField]
    private GameObject _help;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) {
            if (_help.active) {
                HideHelp();
            } else {
                ShowHelp();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return)) {
            if (_help.active) {
                HideHelp();
            } else {
                StartGame();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (_help.active) {
                HideHelp();
            } else {
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
