using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject blackImage;
    AudioManagerScript audioManager;
    float fadeDuration = 1.5f; // Duration for the fade effect
    Coroutine fadeCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        if(audioManager == null)
        {
            audioManager = GameObject.Find("AudioManager").GetComponent<AudioManagerScript>();
        }
        audioManager.musicSource.Stop();
        audioManager.musicSource.clip = audioManager.gameOverBGM;
        audioManager.musicSource.loop = false;
        audioManager.musicSource.Play();
        fadeCoroutine = StartCoroutine(FadeImage(1f, 0f)); // Start fading in the black image
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
    public void QuitGame()
    {
        SceneManager.LoadScene("Start-Menu-Scene");
    }
}
