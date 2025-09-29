using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class EndSceneManager : MonoBehaviour
{
    private AudioManagerScript _audioManager;
    public GameObject CutsceneDisplay;
    public GameObject CutsceneOBJ;

    private void Start()
    {
        if(_audioManager == null && GameObject.Find("AudioManager") != null)
        {
            _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManagerScript>();
        }
        _audioManager.MusicSource.Stop();
        CutsceneDisplay.SetActive(true);
        CutsceneOBJ.SetActive(true);

        CutsceneOBJ.GetComponent<VideoPlayer>().loopPointReached += OnVideoEnd;
    }
    void OnVideoEnd(VideoPlayer vp)
    {
        CutsceneDisplay.SetActive(false);
        CutsceneOBJ.SetActive(false);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("Start-Menu-Scene");
    }
}
