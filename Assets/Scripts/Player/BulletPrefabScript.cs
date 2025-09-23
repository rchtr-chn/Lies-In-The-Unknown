using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletPrefabScript : MonoBehaviour
{
    Slider staminaBar;
    Camera mainCamera;
    Vector3 mousePos;
    Rigidbody2D rb;
    float damage;
    public float force = 5f;
    bool isFullCharge = false; // Track if the bullet is fully charged
    public GameObject player;

    public LayerMask environmentLayer;

    StaminaBarScript staminaBarScript;

    private void Start()
    {
        if (!player)
        {
            player = GameObject.Find("Player");
        }
        if (!staminaBar)
        {
            staminaBar = GameObject.Find("stamina-bar").GetComponent<Slider>();
        }
        if(!staminaBarScript)
        {
            staminaBarScript = GameObject.Find("stamina-bar").GetComponent<StaminaBarScript>();
        }
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();

        damage = GetDamage();
        staminaBarScript.DepleteBar(); // Deplete the stamina bar when the bullet is fired

        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; // Ensure z is zero for 2D
        Vector2 direction = (mousePos - transform.position).normalized;
        Vector3 rotaion = transform.position - mousePos;
        rb.velocity = direction * force;
        float angle = Mathf.Atan2(rotaion.y, rotaion.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); // Adjust rotation to face the mouse position
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) >= 50f)
        {
            Destroy(gameObject); // Destroy the bullet if it goes too far from the player
        }
    }

    float GetDamage()
    {
        if(staminaBar.value >= 99.9f)
        {
            isFullCharge = true; // Set full charge state if stamina is full
            return 10f;
        }
        else
        {
            return Random.Range(0f, 3.5f);
        }
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            BossHealthManager enemyHealthManager = col.gameObject.GetComponentInParent<BossHealthManager>();
            if (enemyHealthManager != null)
            {
                Debug.Log("Enemy hit with damage: " + damage);
                enemyHealthManager.TakeDamage(Mathf.FloorToInt(damage), isFullCharge);

                if (isFullCharge)
                {
                    isFullCharge = false; // Reset the full charge state after dealing damage
                }
            }
            else
            {
                Debug.Log("Enemy does not have a BossHealthManager component.");
            }
        }
        if(col.gameObject.layer == LayerMask.NameToLayer("Environment") || col.gameObject.CompareTag("Enemy"))
        {
            // If the bullet hits an environment object, destroy the bullet
            Destroy(gameObject);
        }
    }
}
