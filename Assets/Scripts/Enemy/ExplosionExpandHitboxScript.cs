using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExplosionExpandHitboxScript : MonoBehaviour
{
    public BoxCollider2D BoxCollider;
    public Vector2 TargetHitboxSize = new Vector2(4f, 4f);
    public float ExpansionDuration = 2f;
    public float ExplosionDamage = 5f;

    private BossHealthManager _bossHealthManager;
    private AudioManagerScript _audioManager;

    private void Start()
    {
        if (!_audioManager && GameObject.Find("AudioManager") != null)
            _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManagerScript>();

        if (!_bossHealthManager)
            _bossHealthManager = GameObject.FindGameObjectWithTag("Enemy_boss").GetComponent<BossHealthManager>();

        if (!BoxCollider)
            BoxCollider = GetComponent<BoxCollider2D>();

        StartCoroutine(ExpandOverTime());
        _audioManager.PlaySfx(_audioManager.FirstBossAttackSfx); // Play explosion sound effect
    }

    private void Update()
    {
        if(_bossHealthManager.isEnraged)
        {
            ExplosionDamage = 10f; // Increase explosion damage when the boss is enraged
        }
    }

    private IEnumerator ExpandOverTime()
    {
        Vector2 startSize = BoxCollider.size;
        float elapsed = 0f;

        while (elapsed < ExpansionDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / ExpansionDuration;
            BoxCollider.size = Vector2.Lerp(startSize, TargetHitboxSize, t);
            yield return null;
        }

        BoxCollider.size = TargetHitboxSize; // Ensure final size
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            PlayerHealthManager playerHealthManager = col.gameObject.GetComponentInParent<PlayerHealthManager>();
            if (playerHealthManager != null)
            {
                // Assuming the explosion deals full damage
                playerHealthManager.TakeDamage(Mathf.FloorToInt(ExplosionDamage));
            }
        }
    }
}
