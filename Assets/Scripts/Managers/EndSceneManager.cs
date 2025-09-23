using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class EndSceneManager : MonoBehaviour
{
    AudioManagerScript audioManager;
    public GameObject cutSceneDisplay;
    public GameObject cutSceneOBJ;

    private void Start()
    {
        if(audioManager == null && GameObject.Find("AudioManager") != null)
        {
            audioManager = GameObject.Find("AudioManager").GetComponent<AudioManagerScript>();
        }
        audioManager.musicSource.Stop();
        cutSceneDisplay.SetActive(true);
        cutSceneOBJ.SetActive(true);

        cutSceneOBJ.GetComponent<VideoPlayer>().loopPointReached += OnVideoEnd;
    }
    void OnVideoEnd(VideoPlayer vp)
    {
        cutSceneDisplay.SetActive(false);
        cutSceneOBJ.SetActive(false);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("Start-Menu-Scene");
    }
}
