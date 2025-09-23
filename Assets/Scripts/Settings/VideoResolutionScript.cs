using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class VideoResolutionScript : MonoBehaviour
{
    public Resolution[] resolutions;
    public List<string> resolutionOptions = new List<string>();
    int currentResolution;
    public Text resolutionText;

    private void Start()
    {
        // Get all available resolutions
        resolutions = Screen.resolutions;

        // Populate the resolution options list
        foreach(Resolution res in resolutions)
        {
            // Check if the resolution is already in the list to avoid duplicates
            string resolutionString = $"{res.width} x {res.height}";
            if (!resolutionOptions.Contains(resolutionString))
            {
                resolutionOptions.Add(resolutionString);
            }
        }

        //int resCount = resolutions.Length;
        currentResolution = PlayerPrefs.GetInt("Resolution", resolutions.Length-1);
        Debug.Log(resolutionOptions.Count);
        SetNewResolution(currentResolution);
    }

    public void ChangeResolution()
    {
        currentResolution = (currentResolution + 1) % resolutions.Length;
        SetNewResolution(currentResolution);
    }

    void SetNewResolution(int index)
    {
        Screen.SetResolution(resolutions[index].width, resolutions[index].height, Screen.fullScreenMode);
        resolutionText.text = $"{resolutions[index].width} x {resolutions[index].height}";
    }
}
