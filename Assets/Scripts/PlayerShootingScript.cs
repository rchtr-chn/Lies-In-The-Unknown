using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShootingScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public StaminaBarScript staminaBarScript;
    public Animator animator;
    public AudioManagerScript audioManager;

    private void Start()
    {
        if (audioManager == null && GameObject.Find("AudioManager") != null)
        {
            audioManager = GameObject.Find("AudioManager").GetComponent<AudioManagerScript>();
        }
        if (!animator)
        {
            animator = GameObject.Find("Player").GetComponent<Animator>();
        }
        if (!staminaBarScript)
        {
            staminaBarScript = GameObject.Find("stamina-bar").GetComponent<StaminaBarScript>();
        }
        if (!bulletPrefab)
        {
            bulletPrefab = Resources.Load<GameObject>("Prefabs/Player/Bullet");
        }
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            animator.SetBool("LMBinputted", true);
        }
        else
        {
            animator.SetBool("LMBinputted", false);
        }
    }

    public void Shoot(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (bulletPrefab)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                audioManager.PlaySfx(audioManager.playerAttackSfx);
            }
            else
            {
                Debug.LogError("Bullet prefab not found!");
            }
        }
    }
}
