using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public GameObject startMenuUI;
    public GameObject settingsUI;
    bool isSettingsOpen = false;

    AudioManagerScript audioManager;

    private void Awake()
    {
        if (GameObject.Find("AudioManager") != null)
        {
            audioManager = GameObject.Find("AudioManager").GetComponent<AudioManagerScript>();
        }
        else
        {
            Debug.LogError("AudioManager not found in the scene.");
        }

        if (settingsUI == null)
        {
            settingsUI = GameObject.Find("SettingsUI");
        }
        settingsUI.SetActive(false); // Ensure the settings UI is initially inactive
    }

    public void StartGame()
    {
        audioManager.musicSource.Stop();
        SceneManager.LoadScene("Pre-Tutorial-Cutscene");
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
