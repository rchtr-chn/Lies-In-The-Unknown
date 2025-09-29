using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public GameObject StartMenuUI;
    public GameObject SettingsUI;
    private bool _isSettingsOpen = false;

    private AudioManagerScript _audioManager;

    private void Awake()
    {
        if (GameObject.Find("AudioManager") != null)
        {
            _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManagerScript>();
        }
        else
        {
            Debug.LogError("AudioManager not found in the scene.");
        }

        if (SettingsUI == null)
        {
            SettingsUI = GameObject.Find("settings-canvas");
        }
        SettingsUI.SetActive(false); // Ensure the settings UI is initially inactive
    }

    public void StartGame()
    {
        //audioManager.musicSource.Stop();
        SceneManager.LoadScene("Tutorial");
    }
    public void OpenSettings()
    {
        StartMenuUI.SetActive(_isSettingsOpen);
        SettingsUI.SetActive(!_isSettingsOpen);
        _isSettingsOpen = !_isSettingsOpen;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
