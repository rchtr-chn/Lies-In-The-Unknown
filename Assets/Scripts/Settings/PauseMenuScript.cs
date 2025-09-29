using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject PauseMenuUI;
    public GameObject SettingsMenuUI;
    public bool IsPaused = false;
    public bool IsSettingsOpen = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !IsSettingsOpen)
        {
            if (PauseMenuUI.activeInHierarchy)
            {
                IsPaused = false;
                PauseMenuUI.SetActive(false);
                Time.timeScale = 1f; // Resume the game
            }
            else
            {
                IsPaused = true;
                PauseMenuUI.SetActive(true);
                Time.timeScale = 0f; // Pause the game
            }
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Resume the game
    }
    public void Settings()
    {
        if(IsSettingsOpen)
        {
            PauseMenuUI.SetActive(true);
            SettingsMenuUI.SetActive(false);
            IsSettingsOpen = false;
        }
        else
        {
            PauseMenuUI.SetActive(false);
            SettingsMenuUI.SetActive(true);
            IsSettingsOpen = true;
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Start-Menu-Scene");
    }
}
