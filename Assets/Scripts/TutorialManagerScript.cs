using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManagerScript : MonoBehaviour
{
    public GameObject[] tutorialObjects;
    Coroutine tutorialCoroutine;
    Coroutine fadeCoroutine;
    readonly float fadeDuration = 1.5f;
    public GameObject blackImage;

    private void Awake()
    {
        if(blackImage == null)
        {
            blackImage = GameObject.Find("BlackImage");
        }
        blackImage.SetActive(false); // Ensure the black image is initially inactive
        if (tutorialObjects.Length == 0)
        {
            Debug.LogError("No tutorial objects assigned in the inspector.");
        }
        else
        {
            foreach (GameObject obj in tutorialObjects)
            {
                obj.SetActive(false); // Ensure all tutorial objects are initially inactive
            }
        }
    }
    void Start()
    {
        tutorialCoroutine = StartCoroutine(TutorialCoroutine());
    }

    IEnumerator TutorialCoroutine()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("Tutorial started");
        // Add your tutorial logic here

        tutorialObjects[0].SetActive(true);
        bool aPressed = false;
        bool dPressed = false;
        while (!aPressed || !dPressed)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                aPressed = true;
                Debug.Log("A key pressed");
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                dPressed = true;
                Debug.Log("D key pressed");
            }
            yield return null; // Wait for the next frame
        }
        tutorialObjects[0].SetActive(false);
        Debug.Log("Movement keys pressed, proceeding to next step");

        yield return new WaitForSeconds(1f);

        tutorialObjects[1].SetActive(true);
        bool spacePressed = false;
        while (!spacePressed)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                spacePressed = true;
                Debug.Log("Space key pressed");
            }
            yield return null; // Wait for the next frame
        }
        tutorialObjects[1].SetActive(false);
        Debug.Log("Jump key pressed, proceeding to next step");

        yield return new WaitForSeconds(1f);

        tutorialObjects[2].SetActive(true);
        bool lMBPressed = false;
        while (!lMBPressed)
        {
            if (Input.GetMouseButtonDown(0)) // Left Mouse Button
            {
                lMBPressed = true;
                Debug.Log("Left Mouse Button pressed");
            }
            yield return null; // Wait for the next frame
        }
        tutorialObjects[2].SetActive(false);
        Debug.Log("Left Mouse Button pressed, proceeding to next step");

        yield return new WaitForSeconds(1f);

        tutorialObjects[3].SetActive(true);
        bool ePressed = false;
        bool qPressed = false;
        while (!ePressed || !qPressed)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ePressed = true;
                Debug.Log("E key pressed");
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                qPressed = true;
                Debug.Log("Q key pressed");
            }
            yield return null; // Wait for the next frame
        }
        tutorialObjects[3].SetActive(false);
        Debug.Log("Interaction keys pressed, proceeding to next step");


        yield return new WaitForSeconds(3.5f);

        blackImage.SetActive(true); // Activate the black image for fade effect
        fadeCoroutine = StartCoroutine(FadeImage(0f, 1f)); // Fade to black
        Debug.Log("Tutorial ended");
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
        yield return new WaitForSeconds(1f); // Wait before loading the next scene
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
            fadeCoroutine = null; // Reset coroutine reference
        }

        SceneManager.LoadScene("Gameplay");
    }
}
