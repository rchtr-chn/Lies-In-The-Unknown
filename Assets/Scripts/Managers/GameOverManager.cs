using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject BlackImage;
    private AudioManagerScript _audioManager;
    private float _fadeDuration = 1.5f; // Duration for the fade effect
    private Coroutine _fadeCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        if(_audioManager == null)
        {
            _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManagerScript>();
        }
        _audioManager.MusicSource.Stop();
        _audioManager.MusicSource.clip = _audioManager.GameOverBGM;
        _audioManager.MusicSource.loop = false;
        _audioManager.MusicSource.Play();
        _fadeCoroutine = StartCoroutine(FadeImage(1f, 0f)); // Start fading in the black image
    }
    public IEnumerator FadeImage(float fromAlpha, float toAlpha)
    {
        Image targetImage = BlackImage.GetComponent<Image>();

        yield return new WaitForSeconds(0.5f); // Wait before starting the fade

        float elapsed = 0f;
        Color color = targetImage.color;

        while (elapsed < _fadeDuration)
        {
            float alpha = Mathf.Lerp(fromAlpha, toAlpha, elapsed / _fadeDuration);
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
    public void QuitGame()
    {
        SceneManager.LoadScene("Start-Menu-Scene");
    }
}
