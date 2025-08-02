using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoWindowModeScript : MonoBehaviour
{
    public List<string> windowModes = new List<string> { "Windowed", "Borderless", "Fullscreen" };
    string currentWindowMode;
    public Text windowModeText;
    void Start()
    {
        //gets playerprefs for saved window mode and sets it initially
        currentWindowMode = PlayerPrefs.GetString("WindowMode", "Fullscreen");
        windowModeText.text = currentWindowMode;
        SetWindowMode(currentWindowMode);
    }

    public void ChangeWindowMode()
    {
        currentWindowMode = windowModes[(windowModes.IndexOf(currentWindowMode) + 1) % windowModes.Count];
        windowModeText.text = currentWindowMode;
        SetWindowMode(currentWindowMode);
    }

    void SetWindowMode(string mode)
    {
        switch (mode)
        {
            case "Windowed":
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
            case "Borderless":
                Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
                break;
            case "Fullscreen":
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
            default:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
        }
    }
}
