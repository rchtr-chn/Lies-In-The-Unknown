using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShootingScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public StaminaBarScript staminaBarScript;

    private void Start()
    {
        if (!staminaBarScript)
        {
            staminaBarScript = GameObject.Find("stamina-bar").GetComponent<StaminaBarScript>();
        }
        if (!bulletPrefab)
        {
            bulletPrefab = Resources.Load<GameObject>("Prefabs/Player/Bullet");
        }
    }

    public void Shoot(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (bulletPrefab)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            }
            else
            {
                Debug.LogError("Bullet prefab not found!");
            }
        }
    }
}
