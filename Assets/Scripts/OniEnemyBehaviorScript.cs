using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OniEnemyBehaviorScript : MonoBehaviour
{

    public float bossSpeed = 5f;
    public float maxDistanceToChase = 5f;

    public float elapsedTime = 0f;
    public float attackCooldown = 1f;
    bool onCooldown = false;

    Coroutine attackCoroutine;

    public Transform playerTransform;
    public Transform bulletSpawnerTransform;

    public GameObject oniBullet;

    private void Start()
    {
        if (playerTransform == null)
        {
            playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        }
        if (bulletSpawnerTransform == null)
        {
            bulletSpawnerTransform = GameObject.Find("BossBulletSpawner").GetComponent<Transform>();
        }
    }

    private void Update()
    {
        float distanceBetweenEntity = Vector2.Distance(transform.position, playerTransform.position);

        if(distanceBetweenEntity > maxDistanceToChase)
        {
            ChasePlayer();
            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }
        }
        else if(!onCooldown)
        {
            attackCoroutine = StartCoroutine(StartAttack());
            onCooldown = true;
        }

        if (onCooldown)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= attackCooldown)
            {
                onCooldown = false;
                elapsedTime = 0f;
            }
        }
    }

    void ChasePlayer()
    {
        Vector2 playerPos = playerTransform.position;
        transform.position = Vector2.MoveTowards(transform.position, playerPos, bossSpeed * Time.deltaTime);
    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(0.3f);

        GameObject enemyBullet = Instantiate(oniBullet, bulletSpawnerTransform.position, bulletSpawnerTransform.rotation);

        yield return null;
    }
}
