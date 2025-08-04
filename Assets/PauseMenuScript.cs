using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;
    public bool isPaused = false;
    public bool isSettingsOpen = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isSettingsOpen)
        {
            if (pauseMenuUI.activeInHierarchy)
            {
                isPaused = false;
                pauseMenuUI.SetActive(false);
                Time.timeScale = 1f; // Resume the game
            }
            else
            {
                isPaused = true;
                pauseMenuUI.SetActive(true);
                Time.timeScale = 0f; // Pause the game
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Resume the game
    }
    public void Settings()
    {
        if(isSettingsOpen)
        {
            pauseMenuUI.SetActive(true);
            settingsMenuUI.SetActive(false);
            isSettingsOpen = false;
        }
        else
        {
            pauseMenuUI.SetActive(false);
            settingsMenuUI.SetActive(true);
            isSettingsOpen = true;
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Start-Menu-Scene");
    }
}
