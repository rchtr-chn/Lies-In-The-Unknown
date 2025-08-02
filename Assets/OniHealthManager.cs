using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OniHealthManager : MonoBehaviour
{
    public int maxHealth = 100;
    int health;
    public int maxShield = 50;
    int shield;

    bool isEnraged = false; // Optional: Track if the Oni is enraged

    private void Start()
    {
        health = maxHealth;
        shield = maxShield;
    }

    private void Update()
    {
        // Optional: You can add logic here to check health and shield status, or update UI elements
        if (health < 50)
        {
            if (!isEnraged)
            {
                isEnraged = true;
                // Trigger enraged state, e.g., change color, increase speed, etc.
                Debug.Log("Oni is enraged!");
                // You can add more logic here to change the Oni's behavior when enraged
            }
            if (health <= 0)
            {
                Die();
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (shield > 0)
        {
            shield -= damage;
            if (shield < 0)
            {
                health += shield; // If shield goes below 0, reduce health by the remaining damage
                shield = 0; // Ensure shield doesn't go below 0
            }
        }
        else
        {
            health -= damage;
        }
    }

    public void Die()
    {
        Debug.Log("Oni is dead!");
        // Handle death logic here, e.g., play animation, disable game object, etc.}
    }
}
