using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class PreTutorialSceneManager : MonoBehaviour
{
    public VideoPlayer vPlayer;
    AudioManagerScript audioManager;

    private void Start()
    {
        if(audioManager == null)
        {
            audioManager = GameObject.Find("AudioManager").GetComponent<AudioManagerScript>();
        }

        if (vPlayer != null)
        {
            vPlayer = GameObject.Find("VideoPlayerObject").GetComponent<VideoPlayer>();

            vPlayer.loopPointReached += OnVideoEnd;
        }

    }

    void OnVideoEnd(VideoPlayer vp)
    {
        audioManager.musicSource.Play();
        SceneManager.LoadScene("Tutorial");
    }
}
