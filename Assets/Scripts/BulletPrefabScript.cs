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

    private void Start()
    {
        if(!staminaBar)
        {
            staminaBar = GameObject.Find("stamina-bar").GetComponent<Slider>();
        }
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();

        damage = GetDamage();


        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; // Ensure z is zero for 2D
        Vector2 direction = (mousePos - transform.position).normalized;
        Vector3 rotaion = transform.position - mousePos;
        rb.velocity = direction * force;
        float angle = Mathf.Atan2(rotaion.y, rotaion.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90)); // Adjust rotation to face the mouse position
    }

    float GetDamage()
    {
        if(staminaBar.value >= 99.9f)
        {
            return 10f;
        }
        else if (staminaBar.value >= 66.6f)
        {
            return 3.5f;
        }
        else
        {
            return Random.Range(0f, 2f);
        }
    }
}
