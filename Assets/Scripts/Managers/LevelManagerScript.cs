using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class LevelManagerScript : MonoBehaviour
{
    public GameObject LevelOne;
    public GameObject LevelTwo;
    public GameObject FirstBoss;
    public GameObject SecondBoss;

    public GameObject LevelOneBG;

    public GameObject BlackImage;
    public float FadeDuration = 1.5f;
    private Coroutine _fadeCoroutine;

    public Slider BossHealthBar;
    public Slider BossShieldBar;
    public Slider PlayerHealthBar;

    public Transform PlayerTransform;

    public AudioManagerScript AudioManager;
    public GameObject DialogueManager;

    private Coroutine _cutsceneCoroutine;
    public GameObject CutsceneDisplay;
    public GameObject CutsceneOBJ;


    private void Awake()
    {
        if(DialogueManager == null)
        {
            DialogueManager = GameObject.Find("DialogueManager");
        }
        if (AudioManager == null && GameObject.Find("AudioManager") != null)
        {
            AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManagerScript>();
        }
        if (LevelOneBG == null)
        {
            LevelOneBG = GameObject.Find("background-1");
        }
        if (BlackImage == null)
        {
            BlackImage = GameObject.Find("BlackImage");
        }
        BlackImage.SetActive(false); // Ensure the black image is initially inactive
    }

    private void Start()
    {
        if(PlayerTransform == null)
        {
            PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        PlayerTransform.position = new Vector2(0, -1.5f);

        LevelOne.SetActive(true);
        FirstBoss.SetActive(true);



        AudioManager.MusicSource.Stop();
        AudioManager.MusicSource.clip = AudioManager.LevelOneBGM;
        AudioManager.MusicSource.loop = true;
        AudioManager.MusicSource.Play();
    }

    public void CutscenePostOni()
    {
        AudioManager.MusicSource.Stop();
        CutsceneDisplay.SetActive(true);
        CutsceneOBJ.SetActive(true);

        CutsceneOBJ.GetComponent<VideoPlayer>().loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        CutsceneDisplay.SetActive(false);
        CutsceneOBJ.SetActive(false);
        InitiateSecondLevel();
    }

    public void InitiateSecondLevel()
    {
        AudioManager.MusicSource.Stop();
        AudioManager.MusicSource.clip = AudioManager.LevelTwoBGM;
        AudioManager.MusicSource.loop = true;
        AudioManager.MusicSource.Play();

        LevelOneBG.SetActive(false);
        UpdateBossHealthBar();
        UpdatePlayerHealthBar();

        BlackImage.SetActive(true);

        PlayerTransform.position = new Vector2(0, -1.5f);

        LevelOne.SetActive(false);
        LevelTwo.SetActive(true);
        SecondBoss.SetActive(true);

        _fadeCoroutine = StartCoroutine(FadeImage(1f, 0f));

        DialogueManager.SetActive(true); // Activate the dialogue manager for level two
    }

    public IEnumerator FadeImage(float fromAlpha, float toAlpha)
    {
        Image targetImage = BlackImage.GetComponent<Image>();

        yield return new WaitForSeconds(0.5f); // Wait before starting the fade

        float elapsed = 0f;
        Color color = targetImage.color;

        while (elapsed < FadeDuration)
        {
            float alpha = Mathf.Lerp(fromAlpha, toAlpha, elapsed / FadeDuration);
            color.a = alpha;
            targetImage.color = color;

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure final value is exact
        color.a = toAlpha;
        targetImage.color = color;
        BlackImage.SetActive(false); // Disable the black image after fading out
        if (_fadeCoroutine != null)
        {
            StopCoroutine(_fadeCoroutine);
            _fadeCoroutine = null; // Reset coroutine reference
        }
    }

    void UpdateBossHealthBar()
    {
        if (BossHealthBar == null)
        {
            BossHealthBar = GameObject.Find("boss-healthBar").GetComponent<Slider>();
        }
        if (BossShieldBar == null)
        {
            BossShieldBar = GameObject.Find("boss-shieldBar").GetComponent<Slider>();
        }

        BossHealthManager bossHealthScript = SecondBoss.GetComponent<BossHealthManager>();

        BossHealthBar.maxValue = bossHealthScript.MaxHealth;
        BossShieldBar.maxValue = bossHealthScript.MaxShield;
        BossHealthBar.value = BossHealthBar.maxValue;
        BossShieldBar.value = BossShieldBar.maxValue;

        Debug.Log("Boss health and shield bars updated.");
    }

    void UpdatePlayerHealthBar()
    {
        if (PlayerHealthBar == null)
        {
            PlayerHealthBar = GameObject.Find("player-healthBar").GetComponent<Slider>();
        }
        PlayerHealthManager playerHealthScript = PlayerTransform.GetComponent<PlayerHealthManager>();
        PlayerHealthBar.maxValue = playerHealthScript.maxHealth;
        PlayerHealthBar.value = playerHealthScript.maxHealth;
        Debug.Log("Player health bar updated.");
    }

    public void AcceptCutscene()
    {
        SceneManager.LoadScene("You-Win-Cutscene");
    }

    public void RejectCutscene()
    {
        SceneManager.LoadScene("You-Lose-Cutscene");
    }
}
