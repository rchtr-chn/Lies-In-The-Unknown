using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public GameObject startMenuUI;
    public GameObject settingsUI;
    bool isSettingsOpen = false;

    private void Awake()
    {

        if (settingsUI == null)
        {
            settingsUI = GameObject.Find("SettingsUI");
        }
        settingsUI.SetActive(false); // Ensure the settings UI is initially inactive
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void OpenSettings()
    {
        startMenuUI.SetActive(isSettingsOpen);
        settingsUI.SetActive(!isSettingsOpen);
        isSettingsOpen = !isSettingsOpen;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
