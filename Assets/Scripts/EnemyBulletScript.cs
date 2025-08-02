using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    public float bulletForce = 40f;

    Transform playerTransform;
    Rigidbody2D rb;
    public GameObject explosionEffect;

    Vector2 playerPos;

    private void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        playerPos = playerTransform.position;
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Vector2 direction = (playerPos - (Vector2)transform.position).normalized;
        Vector3 rotation = transform.position - (Vector3)playerPos;
        transform.position = Vector2.MoveTowards(transform.position, playerPos, bulletForce * Time.deltaTime);

        float angle = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); // Adjust rotation to face the player position


        if (Vector2.Distance(transform.position, playerPos) < 0.1f)
        {
            ExplodeBullet();
        }
    }

    void ExplodeBullet()
    {
        GameObject explosion = Instantiate(explosionEffect, transform.position + new Vector3(0f, -4f, 0f), Quaternion.identity);
        Destroy(gameObject);
        Destroy(explosion, 1.61f); // Destroy the explosion effect after 1 second
    }
}
