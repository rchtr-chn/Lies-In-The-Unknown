using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBarScript : MonoBehaviour
{
    public Slider staminaBar;
    private Coroutine regenerationCoroutine;
    public RectTransform rectTransform;
    public GameObject player;
    bool staminaDepleted = false;

    private void Start()
    {
        if(!staminaBar)
            staminaBar = GetComponent<Slider>();
        if (!player)
            GameObject.Find("Player");
        if(!rectTransform)
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
        }
    }

    private void Update()
    {
        if (staminaDepleted && regenerationCoroutine == null)
        {
            regenerationCoroutine = StartCoroutine(RegenerateBar());
        }

        RecolorFill();
    }

    private void LateUpdate()
    {
        AdjustPosition();
    }
    public void DepleteBar()
    {
        staminaBar.value = 0.0001f;
        if (regenerationCoroutine != null)
        {
            StopCoroutine(regenerationCoroutine); // Stop any ongoing regeneration coroutine
            regenerationCoroutine = null; // Reset the coroutine reference
        }
        staminaDepleted = true; // Set the flag to indicate stamina is depleted
    }

    IEnumerator RegenerateBar()
    {
        yield return new WaitForSeconds(0.7f); // Wait for 1 second before starting regeneration

        float elapsedTime = 0f; // Time elapsed since the start of the coroutine
        float duration = 1.5f; // Duration to fill the stamina bar


        while (staminaBar.value < staminaBar.maxValue)
        {
            staminaBar.value = Mathf.Lerp(0f, 100f, Mathf.Clamp01(elapsedTime / duration));

            staminaBar.fillRect.GetComponent<Image>().color = Color.Lerp(Color.red, Color.white, elapsedTime / duration); // Change color from red to yellow as it fills

            elapsedTime += Time.deltaTime;
            //Debug.Log(staminaBar.value);
            yield return null; // Wait for the next frame
        }

        staminaBar.value = 100f; // Ensure the bar is fully filled at the end
        staminaDepleted = false; // Reset the flag
    }


    void AdjustPosition()
    {
        Vector2 worldPos = player.transform.position + new Vector3(-2.25f, .25f, 0);
        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPos); // Convert world position to screen position

        rectTransform.position = screenPos; // Set the position of the UI element
    }


    void RecolorFill()
    {
        if (staminaBar.value <= 1)
        {
            staminaBar.fillRect.GetComponent<Image>().color = Color.red; // Change color to red when depleted
        }
        if(staminaBar.value == 100)
        {
            staminaBar.fillRect.GetComponent<Image>().color = Color.yellow;
        }
    }
}
