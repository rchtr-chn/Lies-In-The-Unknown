using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitboxManagerScript : MonoBehaviour
{
    public Collider2D playerHitbox;

    private void Start()
    {
        if (playerHitbox == null)
        {
            playerHitbox = GameObject.Find("Player-Hitbox").GetComponent<Collider2D>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy_Attack"))
        {
            Debug.Log("Attacked!");
        }
    }
}
