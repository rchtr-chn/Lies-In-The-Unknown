using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExplosionExpandHitboxScript : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public Vector2 targetSize = new Vector2(4f, 4f);
    public float duration = 2f;
    public float explosionDamage = 25f;

    BossHealthManager bossHealthManager;

    private void Start()
    {
        if(!bossHealthManager)
            bossHealthManager = GameObject.FindGameObjectWithTag("Enemy_boss").GetComponent<BossHealthManager>();

        if (!boxCollider)
            boxCollider = GetComponent<BoxCollider2D>();

        StartCoroutine(ExpandOverTime());
    }

    private void Update()
    {
        if(bossHealthManager.isEnraged)
        {
            explosionDamage = 50f; // Increase explosion damage when the boss is enraged
        }
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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            PlayerHealthManager playerHealthManager = col.gameObject.GetComponentInParent<PlayerHealthManager>();
            if (playerHealthManager != null)
            {
                // Assuming the explosion deals full damage
                playerHealthManager.TakeDamage(Mathf.FloorToInt(explosionDamage));
            }
        }
    }
}
