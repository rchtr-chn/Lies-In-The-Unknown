using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnknownEnemyBehaviorscript : MonoBehaviour
{
    public Vector2 rangeA;
    public Vector2 rangeB;

    public Sprite barrageBulletSprite;
    public GameObject barrageBulletPrefab; // Prefab for the barrage bullet

    public SpriteRenderer spriteRenderer;
    public SpriteRenderer[] barrageSpritePlaceholder;
    public Transform[] barrageAttackSpawnPos;

    public float intervalBetweenTeleports = 10f;
    public float attackCooldown = 3f;
    public int barrageCount = 1;
    Coroutine teleportCoroutine;
    Coroutine fadeCoroutine;
    Coroutine barrageCoroutine;

    bool isStunned = false; // Flag to check if the enemy is stunned
    bool isFading = false; // Flag to check if the enemy is currently fading
    bool isEnraged = false; // Flag to check if the enemy is enraged

    public float elapsedTime = 0f;
    public float fadeDuration = 1f;
    bool fadeOut = true; // Set to true for fade out, false for fade in

    BossHealthManager bossHealthManager;

    private void Start()
    {
        if (!bossHealthManager)
        {
            bossHealthManager = GameObject.FindGameObjectWithTag("Enemy_boss").GetComponent<BossHealthManager>();
        }

        if (!spriteRenderer)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        teleportCoroutine = StartCoroutine(TeleportRandomly());
    }

    void Update()
    {
        if (!isStunned && barrageCoroutine == null)
        {
            barrageCoroutine = StartCoroutine(BarrageAttack());
        }

        if(bossHealthManager.health < bossHealthManager.enragedMinimum && !bossHealthManager.isEnraged)
        {
            Debug.Log("Boss is enraged!");
            bossHealthManager.isEnraged = true;
            Enraged(); // Call the method to handle enraged state
        }
    }

    IEnumerator TeleportRandomly()
    {
        while (true)
        {
            // Wait for the specified interval before teleporting
            yield return new WaitForSeconds(intervalBetweenTeleports);
            // Generate a random position within the defined ranges
            float randomX = Random.Range(rangeA.x, rangeB.x);
            float randomY = Random.Range(rangeA.y, rangeB.y);
            Vector2 randomPosition = new Vector2(randomX, randomY);

            isFading = true; // Set to true to indicate fading is in progress
            fadeOut = true; // Set to true for fade out before teleporting
            fadeCoroutine = StartCoroutine(FadeSprite());

            while (fadeCoroutine != null)
            {
                // Wait until the fade coroutine is finished before teleporting
                yield return null;
            }

            // Teleport the enemy to the new position
            transform.position = randomPosition;

            yield return new WaitForSeconds(0.5f);

            fadeOut = false; // Set to false for fade in after teleporting
            fadeCoroutine = StartCoroutine(FadeSprite());
            isFading = false; // Reset fading flag
        }
    }

    IEnumerator FadeSprite()
    {
        float elapsed = 0f;
        Color startColor = spriteRenderer.color;
        float startAlpha = startColor.a;
        float endAlpha = fadeOut ? 0f : 1f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);
            yield return null;
        }

        // Ensure final alpha is set
        spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, endAlpha);
        fadeCoroutine = null; // Reset fade coroutine reference
    }

    IEnumerator BarrageAttack()
    {
        yield return new WaitForSeconds(attackCooldown); // Initial delay before starting the barrage attack

        foreach (SpriteRenderer sr in barrageSpritePlaceholder)
        {
            // Set the sprite for the barrage attack
            sr.sprite = barrageBulletSprite;
            // Wait for a short duration before moving to the next sprite
            if(isEnraged == false)
            {
                yield return new WaitForSeconds(0.5f);
            }
        }

        while(isFading)
        {
            // Wait until the fade coroutine is finished before proceeding with the barrage attack
            yield return null;
        }


        for(int i=0; i < barrageCount; i++)
        {
            int index = 0;

            foreach (Transform spawnPos in barrageAttackSpawnPos)
            {
                // Instantiate the barrage attack at the spawn position
                GameObject bullet = Instantiate(barrageBulletPrefab, spawnPos.position, spawnPos.rotation);
                barrageSpritePlaceholder[index].sprite = null;
                index++;
            }
            yield return new WaitForSeconds(0.3f); // Wait before the next barrage attack
        }

        yield return null;
        barrageCoroutine = null; // Reset barrage coroutine reference
    }

    void Enraged()
    {
        spriteRenderer.color = Color.red; // Change the sprite color to red when enraged
        intervalBetweenTeleports = 5f; // Decrease teleport interval when enraged
        attackCooldown = 1.5f; // Decrease attack cooldown when enraged
        isEnraged = true; // Set the enraged flag to true
        barrageCount = 3; // Increase barrage count when enraged
    }

}
