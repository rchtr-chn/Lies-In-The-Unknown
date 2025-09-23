using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManagerScript : MonoBehaviour
{
    [Header("---------------------- Audio Source ----------------------")]
    public AudioSource musicSource;
    public AudioSource effectSource;


    [Header("----------------------- Audio Clip -----------------------")]
    public AudioClip firstBossMoveSfx;
    public AudioClip firstBossAttackSfx;
    public AudioClip playerJumpSfx;
    public AudioClip playerRejectSfx;
    public AudioClip playerRunSfx;
    public AudioClip playerAttackSfx;
    public AudioClip playerAcceptsSfx;
    public AudioClip secondBossTeleportSfx;
    public AudioClip secondBossAttackSfx;
    public AudioClip shieldBreakSfx;

    public AudioClip carHornSfx;
    public AudioClip carCrashSfx;

    [Header("----------------------- BGM Clips -----------------------")]
    public AudioClip startMenuBGM;
    public AudioClip levelOneBGM;
    public AudioClip levelTwoBGM;
    public AudioClip Ending;
    public AudioClip gameOverBGM;

    [Header("----------------------- Audio Slider -----------------------")]
    public AudioMixer audioMixer;
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider effectSlider;

    public static AudioManagerScript instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        musicSource.clip = startMenuBGM;
        musicSource.loop = true;
        musicSource.Play();
    }
    public void SetMasterVolume()
    {
        Debug.Log("Setting Master Volume: " + masterSlider.value);
        float volume = masterSlider.value;
        audioMixer.SetFloat("Master", Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20);
    }

    public void SetEffectVolume()
    {
        float volume = effectSlider.value;
        audioMixer.SetFloat("Effects", Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20);
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("Music", Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20);
    }

    public void PlaySfx(AudioClip clip)
    {
        if (effectSource != null && clip != null)
        {
            effectSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("Effect source or clip is null");
        }
    }

    public void StopMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }
        else
        {
            Debug.LogWarning("Music source is null");
        }
    }
}
