using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionExpandHitboxScript : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public Vector2 targetSize = new Vector2(4f, 4f);
    public float duration = 2f;

    private void Start()
    {
        if (!boxCollider)
            boxCollider = GetComponent<BoxCollider2D>();

        StartCoroutine(ExpandOverTime());
    }

    private IEnumerator ExpandOverTime()
    {
        Vector2 startSize = boxCollider.size;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            boxCollider.size = Vector2.Lerp(startSize, targetSize, t);
            yield return null;
        }

        boxCollider.size = targetSize; // Ensure final size
    }
}
