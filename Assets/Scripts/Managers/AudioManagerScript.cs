using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManagerScript : MonoBehaviour
{
    [Header("---------------------- Audio Source ----------------------")]
    public AudioSource MusicSource;
    public AudioSource EffectSource;


    [Header("----------------------- Audio Clip -----------------------")]
    public AudioClip FirstBossMoveSfx;
    public AudioClip FirstBossAttackSfx;
    public AudioClip PlayerJumpSfx;
    public AudioClip PlayerRejectSfx;
    public AudioClip PlayerRunSfx;
    public AudioClip PlayerAttackSfx;
    public AudioClip PlayerAcceptsSfx;
    public AudioClip SecondBossTeleportSfx;
    public AudioClip SecondBossAttackSfx;
    public AudioClip ShieldBreakSfx;

    public AudioClip CarHornSfx;
    public AudioClip CarCrashSfx;

    [Header("----------------------- BGM Clips -----------------------")]
    public AudioClip StartMenuBGM;
    public AudioClip LevelOneBGM;
    public AudioClip LevelTwoBGM;
    public AudioClip Ending;
    public AudioClip GameOverBGM;

    [Header("----------------------- Audio Slider -----------------------")]
    public AudioMixer AudioMixer;
    public Slider MasterSlider;
    public Slider MusicSlider;
    public Slider EffectSlider;

    public static AudioManagerScript Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        MusicSource.clip = StartMenuBGM;
        MusicSource.loop = true;
        MusicSource.Play();
    }
    public void SetMasterVolume()
    {
        Debug.Log("Setting Master Volume: " + MasterSlider.value);
        float volume = MasterSlider.value;
        AudioMixer.SetFloat("Master", Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20);
    }

    public void SetEffectVolume()
    {
        float volume = EffectSlider.value;
        AudioMixer.SetFloat("Effects", Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20);
    }

    public void SetMusicVolume()
    {
        float volume = MusicSlider.value;
        AudioMixer.SetFloat("Music", Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20);
    }

    public void PlaySfx(AudioClip clip)
    {
        if (EffectSource != null && clip != null)
        {
            EffectSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("Effect source or clip is null");
        }
    }

    public void StopMusic()
    {
        if (MusicSource != null)
        {
            MusicSource.Stop();
        }
        else
        {
            Debug.LogWarning("Music source is null");
        }
    }
}
