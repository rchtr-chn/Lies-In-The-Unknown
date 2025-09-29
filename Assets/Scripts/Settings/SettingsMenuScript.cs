using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenuScript : MonoBehaviour
{
    public GameObject SettingsMenuUI;
    public PauseMenuScript PauseMenuScript;
    public static SettingsMenuScript Instance;

    //string targetScene = "You-Win-Cutscene";
    //string targetScene2 = "You-Lose-Cutscene";
    private void Awake()
    {
        if (SettingsMenuUI == null)
        {
            SettingsMenuUI = GameObject.Find("start-menu-settings");
        }

        if (Instance == null)
        {
            Instance = this;
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
            SettingsMenuUI.SetActive(true); // Ensure the settings menu is inactive when a new scene is loaded
        }
    }
    //void OnDestroy()
    //{
    //    SceneManager.sceneLoaded -= OnSceneLoaded; // Always clean up
    //}
}
