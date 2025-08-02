using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveScript : MonoBehaviour
{
    Vector2 offset = new Vector2(0, 7f);
    public GameObject player;

    private void Start()
    {
        if (!player)
            player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (player)
        {
            Vector3 playerPosition = player.transform.position;
            transform.position = new Vector3(transform.position.x, playerPosition.y + offset.y, transform.position.z);
        }
        else
        {
            Debug.LogWarning("Player GameObject not found. Please assign the player GameObject in the inspector or ensure it is named 'Player'.");
        }
    }
}
