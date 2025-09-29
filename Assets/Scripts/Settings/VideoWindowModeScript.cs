using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoWindowModeScript : MonoBehaviour
{
    public List<string> WindowModes = new List<string> { "Windowed", "Borderless", "Fullscreen" };
    private string _currentWindowMode;
    public Text WindowModeText;
    void Start()
    {
        //gets playerprefs for saved window mode and sets it initially
        _currentWindowMode = PlayerPrefs.GetString("WindowMode", "Fullscreen");
        WindowModeText.text = _currentWindowMode;
        SetWindowMode(_currentWindowMode);
    }

    public void ChangeWindowMode()
    {
        _currentWindowMode = WindowModes[(WindowModes.IndexOf(_currentWindowMode) + 1) % WindowModes.Count];
        WindowModeText.text = _currentWindowMode;
        SetWindowMode(_currentWindowMode);
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
