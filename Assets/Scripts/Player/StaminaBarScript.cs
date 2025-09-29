using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBarScript : MonoBehaviour
{
    public Slider StaminaBar;
    private Coroutine _regenerationCoroutine;
    public RectTransform RectTransform;
    public GameObject Player;
    private bool _staminaDepleted = false;

    private void Start()
    {
        if(!StaminaBar)
            StaminaBar = GetComponent<Slider>();
        if (!Player)
            GameObject.Find("Player");
        if(!RectTransform)
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
        }
    }

    private void Update()
    {
        if (_staminaDepleted && _regenerationCoroutine == null)
        {
            _regenerationCoroutine = StartCoroutine(RegenerateBar());
        }

        RecolorFill();
    }

    private void LateUpdate()
    {
        AdjustPosition();
    }
    public void DepleteBar()
    {
        StaminaBar.value = 0.0001f;
        if (_regenerationCoroutine != null)
        {
            StopCoroutine(_regenerationCoroutine); // Stop any ongoing regeneration coroutine
            _regenerationCoroutine = null; // Reset the coroutine reference
        }
        _staminaDepleted = true; // Set the flag to indicate stamina is depleted
    }

    IEnumerator RegenerateBar()
    {
        yield return new WaitForSeconds(0.7f); // Wait for 1 second before starting regeneration

        float elapsedTime = 0f; // Time elapsed since the start of the coroutine
        float duration = 1.5f; // Duration to fill the stamina bar


        while (StaminaBar.value < StaminaBar.maxValue)
        {
            StaminaBar.value = Mathf.Lerp(0f, 100f, Mathf.Clamp01(elapsedTime / duration));

            StaminaBar.fillRect.GetComponent<Image>().color = Color.Lerp(Color.red, Color.white, elapsedTime / duration); // Change color from red to yellow as it fills

            elapsedTime += Time.deltaTime;
            //Debug.Log(staminaBar.value);
            yield return null; // Wait for the next frame
        }

        StaminaBar.value = 100f; // Ensure the bar is fully filled at the end
        _staminaDepleted = false; // Reset the flag
    }


    void AdjustPosition()
    {
        Vector2 worldPos = Player.transform.position + new Vector3(-2.25f, .25f, 0);
        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPos); // Convert world position to screen position

        RectTransform.position = screenPos; // Set the position of the UI element
    }


    void RecolorFill()
    {
        if (StaminaBar.value <= 1)
        {
            StaminaBar.fillRect.GetComponent<Image>().color = Color.red; // Change color to red when depleted
        }
        if(StaminaBar.value == 100)
        {
            StaminaBar.fillRect.GetComponent<Image>().color = Color.yellow;
        }
    }
}
