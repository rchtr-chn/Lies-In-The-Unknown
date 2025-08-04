using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdjustBackgroundSize : MonoBehaviour
{
    public Image backgroundImage; // Reference to the background image
    

    private void Start()
    {
        if(backgroundImage == null)
        {
            backgroundImage = GetComponent<Image>();
        }
        backgroundImage.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
    }
}
