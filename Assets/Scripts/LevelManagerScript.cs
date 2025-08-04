using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManagerScript : MonoBehaviour
{
    public GameObject levelOne;
    public GameObject levelTwo;
    public GameObject firstBoss;
    public GameObject secondBoss;

    public GameObject levelOneBG;

    public GameObject blackImage;
    public float fadeDuration = 1.5f;
    Coroutine fadeCoroutine;

    public Slider bossHealthBar;
    public Slider bossShieldBar;
    public Slider playerHealthBar;

    public Transform playerTransform;

    public AudioManagerScript audioManager;
    public GameObject dialogueManager;

    private void Awake()
    {
        if(dialogueManager == null)
        {
            dialogueManager = GameObject.Find("DialogueManager");
        }
        if (audioManager == null && GameObject.Find("AudioManager") != null)
        {
            audioManager = GameObject.Find("AudioManager").GetComponent<AudioManagerScript>();
        }
        if (levelOneBG == null)
        {
            levelOneBG = GameObject.Find("background-1");
        }
        if (blackImage == null)
        {
            blackImage = GameObject.Find("BlackImage");
        }
        blackImage.SetActive(false); // Ensure the black image is initially inactive
    }

    private void Start()
    {
        if(playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        playerTransform.position = new Vector2(0, -1.5f);

        levelOne.SetActive(true);
        firstBoss.SetActive(true);
        //InitiateSecondLevel();

        audioManager.musicSource.Stop();
        audioManager.musicSource.clip = audioManager.levelOneBGM;
        audioManager.musicSource.loop = true;
        audioManager.musicSource.Play();
    }

    public void InitiateSecondLevel()
    {
        audioManager.musicSource.Stop();
        audioManager.musicSource.clip = audioManager.levelTwoBGM;
        audioManager.musicSource.loop = true;
        audioManager.musicSource.Play();

        levelOneBG.SetActive(false);
        UpdateBossHealthBar();
        UpdatePlayerHealthBar();

        blackImage.SetActive(true);

        playerTransform.position = new Vector2(0, -1.5f);

        levelOne.SetActive(false);
        levelTwo.SetActive(true);
        secondBoss.SetActive(true);

        fadeCoroutine = StartCoroutine(FadeImage(1f, 0f));

        dialogueManager.SetActive(true); // Activate the dialogue manager for level two
    }

    public IEnumerator FadeImage(float fromAlpha, float toAlpha)
    {
        Image targetImage = blackImage.GetComponent<Image>();

        yield return new WaitForSeconds(0.5f); // Wait before starting the fade

        float elapsed = 0f;
        Color color = targetImage.color;

        while (elapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(fromAlpha, toAlpha, elapsed / fadeDuration);
            color.a = alpha;
            targetImage.color = color;

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure final value is exact
        color.a = toAlpha;
        targetImage.color = color;
        blackImage.SetActive(false); // Disable the black image after fading out
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
            fadeCoroutine = null; // Reset coroutine reference
        }
    }

    void UpdateBossHealthBar()
    {
        if (bossHealthBar == null)
        {
            bossHealthBar = GameObject.Find("boss-healthBar").GetComponent<Slider>();
        }
        if (bossShieldBar == null)
        {
            bossShieldBar = GameObject.Find("boss-shieldBar").GetComponent<Slider>();
        }

        BossHealthManager bossHealthScript = secondBoss.GetComponent<BossHealthManager>();

        bossHealthBar.maxValue = bossHealthScript.maxHealth;
        bossShieldBar.maxValue = bossHealthScript.maxShield;
        bossHealthBar.value = bossHealthBar.maxValue;
        bossShieldBar.value = bossShieldBar.maxValue;

        Debug.Log("Boss health and shield bars updated.");
    }

    void UpdatePlayerHealthBar()
    {
        if (playerHealthBar == null)
        {
            playerHealthBar = GameObject.Find("player-healthBar").GetComponent<Slider>();
        }
        PlayerHealthManager playerHealthScript = playerTransform.GetComponent<PlayerHealthManager>();
        playerHealthBar.maxValue = playerHealthScript.maxHealth;
        playerHealthBar.value = playerHealthScript.maxHealth;
        Debug.Log("Player health bar updated.");
    }
}
