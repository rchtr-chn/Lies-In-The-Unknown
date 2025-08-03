using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UnknownEnemyBehaviorscript : MonoBehaviour
{
    public Vector2 rangeA;
    public Vector2 rangeB;

    public Sprite barrageBulletSprite;
    public GameObject barrageBulletPrefab; // Prefab for the barrage bullet
    public GameObject acceptOrRejectGroup;

    public SpriteRenderer spriteRenderer;
    public SpriteRenderer[] barrageSpritePlaceholder;
    public Transform[] barrageAttackSpawnPos;

    public float intervalBetweenTeleports = 10f;
    public float attackCooldown = 3f;
    public int barrageCount = 1;
    Coroutine teleportCoroutine;
    Coroutine fadeCoroutine;
    Coroutine barrageCoroutine;
    Coroutine stunCoroutine; // Coroutine for handling stun effect

    public float stunDuration = 5f; // Duration of the stun effect
    bool isStunned = false; // Flag to check if the enemy is stunned
    bool isFading = false; // Flag to check if the enemy is currently fading
    bool isEnraged = false; // Flag to check if the enemy is enraged

    public float elapsedTime = 0f;
    public float fadeDuration = 1f;
    bool fadeOut = true; // Set to true for fade out, false for fade in

    BossHealthManager bossHealthManager;
    Slider shieldBar;

    private void Start()
    {
        if (!bossHealthManager)
        {
            bossHealthManager = GameObject.FindGameObjectWithTag("Enemy_boss").GetComponent<BossHealthManager>();
        }
        if(!shieldBar)
        {
            shieldBar = GameObject.Find("boss-shieldBar").GetComponent<Slider>();
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

        if(bossHealthManager.health <= bossHealthManager.enragedMinimum && !bossHealthManager.isEnraged)
        {
            Debug.Log("Boss is enraged!");
            bossHealthManager.isEnraged = true;
            Enraged(); // Call the method to handle enraged state
        }

        if (bossHealthManager.shield <= 0 && !isStunned && stunCoroutine == null && !isEnraged)
        {
            stunCoroutine = StartCoroutine(StunEnemy(stunDuration));
        }
        else if (bossHealthManager.shield <= 0 && !isStunned && stunCoroutine == null && isEnraged)
        {
            stunCoroutine = StartCoroutine(StunEnragedEnemy());
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

    IEnumerator StunEnemy(float stunDuration)
    {
        if(barrageCoroutine != null)
        {
            StopCoroutine(barrageCoroutine);
            barrageCoroutine = null; // Reset barrage coroutine reference
            foreach (SpriteRenderer sr in barrageSpritePlaceholder)
            {
                sr.sprite = null; // Clear barrage sprites when stunned
            }
        }
        if(teleportCoroutine != null)
        {
            StopCoroutine(teleportCoroutine);
            if(spriteRenderer.color.a < 1f)
            {
                fadeOut = true; // Set to true for fade out before stun
                fadeCoroutine = StartCoroutine(FadeSprite()); // Start fading in immediately
            }
            teleportCoroutine = null; // Reset teleport coroutine reference
        }

        isStunned = true;
        yield return new WaitForSeconds(stunDuration);
        isStunned = false;
        teleportCoroutine = StartCoroutine(TeleportRandomly()); // Restart teleport coroutine after stun
        shieldBar.value = bossHealthManager.shield = bossHealthManager.maxShield; // Restore shield after stun
        Debug.Log("Oni is no longer stunned.");

        stunCoroutine = null; // Reset stun coroutine reference
    }

    IEnumerator StunEnragedEnemy()
    {
        if (barrageCoroutine != null)
        {
            StopCoroutine(barrageCoroutine);
            barrageCoroutine = null; // Reset barrage coroutine reference
            foreach (SpriteRenderer sr in barrageSpritePlaceholder)
            {
                sr.sprite = null; // Clear barrage sprites when stunned
            }
        }
        if (teleportCoroutine != null)
        {
            StopCoroutine(teleportCoroutine);
            teleportCoroutine = null; // Reset teleport coroutine reference
        }
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
            fadeCoroutine = null; // Reset fade coroutine reference
            fadeOut = true; // Set to true for fade out before stun
            fadeCoroutine = StartCoroutine(FadeSprite()); // Start fading in immediately
        }
        transform.position = new Vector2(0, 10.6f); // Move the enemy to a specific position when stunned
        isStunned = true; // Set the stunned flag to true
        Collider2D col = GameObject.Find("unknown-collider").GetComponent<Collider2D>();
        col.enabled = false;
        AcceptOrRejectScript aOR_Script = GameObject.Find("Player").GetComponent<AcceptOrRejectScript>();
        aOR_Script.isDeciding = true; // Set the deciding flag to true to allow player to accept or reject
        while (aOR_Script.isDeciding)
        {
            AcceptOrReject();
            yield return null; // Wait for the next frame while the player is deciding
        }

        acceptOrRejectGroup.SetActive(false);

        if (aOR_Script.isAccepted)
        {
            bossHealthManager.TakeDamage(100, true); // Deal damage to the boss when accepted
        }
        else
        {
            PlayerHealthManager playerHealthManager = GameObject.Find("Player").GetComponent<PlayerHealthManager>();
            playerHealthManager.TakeDamage(100); // Deal damage to the player when rejected
        }

        yield return null;
        isStunned = false; // Reset the stunned flag
        col.enabled = true; // Re-enable the collider after the stun effect
        stunCoroutine = null; // Reset stun coroutine reference

    }

    void AcceptOrReject()
    {
        if (Vector2.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < 5f)
        {
            acceptOrRejectGroup.SetActive(true); // Show the accept or reject UI group
        }
        else
        {
            acceptOrRejectGroup.SetActive(false); // Hide the accept or reject UI group if player is too far
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
            yield return new WaitForSeconds(0.5f); // Wait before the next barrage attack
        }

        yield return null;
        barrageCoroutine = null; // Reset barrage coroutine reference
    }

    void Enraged()
    {
        spriteRenderer.color = Color.red; // Change the sprite color to red when enraged
        intervalBetweenTeleports = 5f; // Decrease teleport interval when enraged
        attackCooldown = 2f; // Decrease attack cooldown when enraged
        isEnraged = true; // Set the enraged flag to true
        barrageCount = 2; // Increase barrage count when enraged
    }

}
