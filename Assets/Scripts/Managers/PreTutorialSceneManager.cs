using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class PreTutorialSceneManager : MonoBehaviour
{
    public VideoPlayer VideoPlayer;
    private AudioManagerScript _audioManager;

    private void Start()
    {
        if(_audioManager == null)
        {
            _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManagerScript>();
        }

        if (VideoPlayer != null)
        {
            VideoPlayer = GameObject.Find("VideoPlayerObject").GetComponent<VideoPlayer>();

            VideoPlayer.loopPointReached += OnVideoEnd;
        }

    }

    void OnVideoEnd(VideoPlayer vp)
    {
        LoadGameplayScene();
    }

    public void LoadGameplayScene()
    {
        _audioManager.MusicSource.Play();
        SceneManager.LoadScene("Gameplay");
    }
}
