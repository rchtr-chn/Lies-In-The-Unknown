using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenuScript : MonoBehaviour
{
    public GameObject settingsMenuUI;
    public PauseMenuScript pauseMenuScript;
    public static SettingsMenuScript instance;

    //string targetScene = "You-Win-Cutscene";
    //string targetScene2 = "You-Lose-Cutscene";
    private void Awake()
    {
        if (settingsMenuUI == null)
        {
            settingsMenuUI = GameObject.Find("start-menu-settings");
        }

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Tutorial")
        {
            settingsMenuUI.SetActive(true); // Ensure the settings menu is inactive when a new scene is loaded
        }
    }
    //void OnDestroy()
    //{
    //    SceneManager.sceneLoaded -= OnSceneLoaded; // Always clean up
    //}
}
