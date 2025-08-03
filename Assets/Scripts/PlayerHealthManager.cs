using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
    public int maxHealth = 100;
    private int health;

    public Slider healthBar;
    public Collider2D playerHitbox;
    Coroutine IFrameCoroutine;

    private void Start()
    {
        if (playerHitbox == null)
        {
            playerHitbox = GameObject.Find("Player-Hitbox").GetComponent<Collider2D>();
        }
        if (healthBar == null)
        {
            healthBar = GameObject.Find("player-healthBar").GetComponent<Slider>();
        }
        healthBar.maxValue = health = maxHealth;
    }

    private void Update()
    {
        if (health <= 0)
        {
            //Die();
        }
    }

    public void TakeDamage(int damage)
    {
        if (IFrameCoroutine == null)
        {
            IFrameCoroutine = StartCoroutine(IFrames());
        }
        health -= damage;
        if (health < 0)
        {
            health = 0; // Ensure health doesn't go below 0
        }
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        healthBar.value = (float)health;
    }

    IEnumerator IFrames()
    {
        Debug.Log("Invincibility frames activated");
        playerHitbox.enabled = false;

        yield return new WaitForSeconds(1f); // Duration of invincibility frames

        Debug.Log("Invincibility frames ended");
        playerHitbox.enabled = true;

        IFrameCoroutine = null; // Reset the coroutine reference
    }
}
