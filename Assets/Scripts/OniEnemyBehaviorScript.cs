using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class OniEnemyBehaviorScript : MonoBehaviour
{

    public float bossSpeed = 5f;
    public float maxDistanceToChase = 5f;

    public float elapsedTime = 0f;
    public float attackCooldown = 1f;
    bool onCooldown = false;

    Coroutine attackCoroutine;
    Coroutine cutSceneCoroutine;

    public Transform playerTransform;
    public Transform bulletSpawnerTransform;

    public GameObject oniBullet;
    Vector2 playerPos;
    Vector2 bossPosOffset;

    public SpriteRenderer oniSr; // Reference to the Oni's SpriteRenderer for color changes

    BossHealthManager bossHealthManager;
    Coroutine stunCoroutine;
    bool isStunned = false;
    public float stunDuration = 5f; // Duration of the stun effect

    public GameObject cutSceneBorder; // Reference to the cutscene canvas, if needed
    public GameObject cutSceneText; // Reference to the cutscene canvas for the Oni's enraged state
    public bool isOnCutscene = false; // Flag to indicate if the Oni is in a cutscene

    Slider shieldBar; // Reference to the shield bar for the Oni

    private void Start()
    {
        if (cutSceneBorder == null)
        {
            cutSceneBorder = GameObject.Find("first-boss-cutscene");
        }
        cutSceneBorder.SetActive(false); // Ensure the cutscene canvas is initially inactive
        if (cutSceneText == null)
        {
            cutSceneText = GameObject.Find("first-boss-cutscene-text");
        }
        cutSceneText.SetActive(false); // Ensure the cutscene text is initially inactive

        if (shieldBar == null)
        {
            shieldBar = GameObject.Find("boss-shieldBar").GetComponent<Slider>();
        }

        if (!oniSr)
        {
            oniSr = GetComponent<SpriteRenderer>();
        }
        bossPosOffset = new Vector2(0f, -9f); // Offset to adjust the bullet spawn position

        if (!bossHealthManager)
        {
            bossHealthManager = GetComponent<BossHealthManager>();
        }
        if (playerTransform == null)
        {
            playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        }
        if (bulletSpawnerTransform == null)
        {
            bulletSpawnerTransform = GameObject.Find("BossBulletSpawner").GetComponent<Transform>();
        }
    }

    private void Update()
    {
        if(bossHealthManager.health < bossHealthManager.enragedMinimum && !bossHealthManager.isEnraged)
        {
            if (cutSceneCoroutine == null)
            {
                cutSceneCoroutine = StartCoroutine(EnragedCutscene());
            }
            bossHealthManager.isEnraged = true;
            Enraged(); // Call the method to handle enraged state
        }
        if (bossHealthManager.health <= 0 && attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null; // Reset attack coroutine reference
        }

        if (bossHealthManager.shield <= 0 && !isStunned && stunCoroutine == null)
        {
            stunCoroutine = StartCoroutine(StunEnemy(stunDuration));
        }

        if (!isStunned)
        {
            FlipAssetX();

            float distanceBetweenEntity = Vector2.Distance(transform.position - (Vector3)bossPosOffset, playerTransform.position);
            playerPos = playerTransform.position;

            if (distanceBetweenEntity > maxDistanceToChase && !isOnCutscene)
            {
                ChasePlayer();
                if (attackCoroutine != null)
                {
                    StopCoroutine(attackCoroutine);
                    attackCoroutine = null;
                }
            }
            else if (!onCooldown && !isOnCutscene)
            {
                attackCoroutine = StartCoroutine(StartAttack());
                onCooldown = true;
            }

            if (onCooldown)
            {
                elapsedTime += Time.deltaTime;
                if (elapsedTime >= attackCooldown)
                {
                    onCooldown = false;
                    elapsedTime = 0f;
                }
            }
        }
        else
        {
            // Optional: Add logic to handle the Oni's behavior while stunned, e.g., stop movement, play animation, etc.
            Debug.Log("Oni is currently stunned and cannot move or attack.");
        }
    }

    void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPos, bossSpeed * Time.deltaTime);
    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(0.3f);

        GameObject enemyBullet = Instantiate(oniBullet, bulletSpawnerTransform.position, bulletSpawnerTransform.rotation);

        yield return null;
    }

    void FlipAssetX()
    {
        if(playerPos.x > transform.position.x)
        {
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
        else if (playerPos.x < transform.position.x)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }

    IEnumerator StunEnemy(float stunDuration)
    {
        isStunned = true;
        yield return new WaitForSeconds(stunDuration);
        isStunned = false;
        shieldBar.value = bossHealthManager.shield = bossHealthManager.maxShield; // Restore shield after stun
        Debug.Log("Oni is no longer stunned.");

        stunCoroutine = null; // Reset stun coroutine reference
    }

    void Enraged()
    {
        bossHealthManager.shieldBar.maxValue = bossHealthManager.shield = bossHealthManager.maxShield = 50;
        oniSr.color = Color.red; // Change color to indicate enraged state
        maxDistanceToChase = 20f;
        bossSpeed = 3f;
    }

    IEnumerator EnragedCutscene()
    {
        yield return new WaitForSeconds(1f);

        isOnCutscene = true;
        cutSceneBorder.SetActive(true);
        cutSceneText.SetActive(true);

        yield return new WaitForSeconds(5f); // Wait for the cutscene to play out

        cutSceneBorder.SetActive(false);
        cutSceneText.SetActive(false);

        cutSceneCoroutine = null; // Reset cutscene coroutine reference
        isOnCutscene = false; // Reset the cutscene flag
        yield return null;
    }
}
