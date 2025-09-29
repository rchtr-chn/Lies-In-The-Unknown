using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoResolutionScript : MonoBehaviour
{
    public Resolution[] Resolutions;
    public List<string> ResolutionOptions = new List<string>();
    private int _currentResolution;
    public Text ResolutionText;

    private void Start()
    {
        // Get all available resolutions
        Resolutions = Screen.resolutions;

        // Populate the resolution options list
        foreach(Resolution res in Resolutions)
        {
            // Check if the resolution is already in the list to avoid duplicates
            string resolutionString = $"{res.width} x {res.height}";
            if (!ResolutionOptions.Contains(resolutionString))
            {
                ResolutionOptions.Add(resolutionString);
            }
        }

        //int resCount = resolutions.Length;
        _currentResolution = PlayerPrefs.GetInt("Resolution", Resolutions.Length-1);
        Debug.Log(ResolutionOptions.Count);
        SetNewResolution(_currentResolution);
    }

    public void ChangeResolution()
    {
        _currentResolution = (_currentResolution + 1) % Resolutions.Length;
        SetNewResolution(_currentResolution);
    }

    void SetNewResolution(int index)
    {
        Screen.SetResolution(Resolutions[index].width, Resolutions[index].height, Screen.fullScreenMode);
        ResolutionText.text = $"{Resolutions[index].width} x {Resolutions[index].height}";
    }
}
