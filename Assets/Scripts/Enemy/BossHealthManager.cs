using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthManager : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;
    public int maxShield = 50;
    public int shield;
    public float enragedMinimum = 50f;

    public bool isEnraged = false; // Optional: Track if the Oni is enraged

    public Slider healthBar;
    public Slider shieldBar;


    private void Start()
    {
        if (healthBar == null)
        {
            healthBar = GameObject.Find("boss-healthBar").GetComponent<Slider>();
        }
        if (shieldBar == null)
        {
            shieldBar = GameObject.Find("boss-shieldBar").GetComponent<Slider>();
        }

        healthBar.maxValue = health = maxHealth;
        shieldBar.maxValue = shield = maxShield;
    }

    private void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(int damage, bool isFullCharged)
    {
        if (shield > 0 && isFullCharged)
        {
            shield -= damage;
            if (shield < 0)
            {
                shield = 0; // Ensure shield doesn't go below 0
                health += shield; // Apply remaining damage to health if shield is depleted
            }
        }
        else if (shield <= 0)
        {
            health -= damage;
        }

        healthBar.value = (float)health;
        shieldBar.value = (float)shield;
    }

    public void Die()
    {
        gameObject.SetActive(false); // Disable the Oni game object
        Debug.Log("Oni is dead!");
        // Handle death logic here, e.g., play animation, disable game object, etc.}
        if(gameObject.name == "Oni-boss")
        {
            LevelManagerScript levelManager = FindObjectOfType<LevelManagerScript>();
            if (levelManager != null)
            {
                levelManager.CutscenePostOni(); // Call the method to initiate the second level
            }
        }
    }
}
