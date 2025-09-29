using UnityEngine;
using UnityEngine.UI;

public class BossHealthManager : MonoBehaviour
{
    public int MaxHealth = 100;
    public int Health;
    public int MaxShield = 50;
    public int Shield;
    public float EnragedMinimumHP = 50f;

    public bool isEnraged = false; // Optional: Track if the Oni is enraged

    public Slider HealthBar;
    public Slider ShieldBar;


    private void Start()
    {
        if (HealthBar == null)
        {
            HealthBar = GameObject.Find("boss-healthBar").GetComponent<Slider>();
        }
        if (ShieldBar == null)
        {
            ShieldBar = GameObject.Find("boss-shieldBar").GetComponent<Slider>();
        }

        HealthBar.maxValue = Health = MaxHealth;
        ShieldBar.maxValue = Shield = MaxShield;
    }

    private void Update()
    {
        if (Health <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(int damage, bool isFullCharged)
    {
        if (Shield > 0 && isFullCharged)
        {
            Shield -= damage;
            if (Shield < 0)
            {
                Shield = 0; // Ensure shield doesn't go below 0
                Health += Shield; // Apply remaining damage to health if shield is depleted
            }
        }
        else if (Shield <= 0)
        {
            Health -= damage;
        }

        HealthBar.value = (float)Health;
        ShieldBar.value = (float)Shield;
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
