using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class OniEnemyBehaviorScript : MonoBehaviour
{

    public float BossSpeed = 5f;
    public float MaxChaseDistance = 5f;

    public float ElapsedTime = 0f;
    public float AttackCooldown = 1f;
    private bool _onCooldown = false;

    private Coroutine _attackCoroutine;
    private Coroutine _cutSceneCoroutine;

    public Transform PlayerTransform;
    public Transform BulletSpawnerTransform;

    public GameObject EnemyBullet;

    private Vector2 _playerPos;
    private Vector2 _bossPosOffset;

    public SpriteRenderer OniSpriteRenderer; // Reference to the Oni's SpriteRenderer for color changes

    private BossHealthManager _bossHealthManager;
    private Coroutine _stunCoroutine;
    private bool _isStunned = false;
    public float StunDuration = 5f; // Duration of the stun effect

    public GameObject CutSceneBorder; // Reference to the cutscene canvas, if needed
    public GameObject CutSceneText; // Reference to the cutscene canvas for the Oni's enraged state
    public bool IsOnCutscene = false; // Flag to indicate if the Oni is in a cutscene

    public AudioManagerScript AudioManager; // Reference to the AudioManager, if needed
    private float _elapsedTime = 0f;
    private bool _audioPlayed = false;

    Slider shieldBar; // Reference to the shield bar for the Oni

    private void Start()
    {
        if (AudioManager == null && GameObject.Find("AudioManager") != null)
        {
            AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManagerScript>();
        }
        if (CutSceneBorder == null)
        {
            CutSceneBorder = GameObject.Find("first-boss-cutscene");
        }
        CutSceneBorder.SetActive(false); // Ensure the cutscene canvas is initially inactive
        if (CutSceneText == null)
        {
            CutSceneText = GameObject.Find("first-boss-cutscene-text");
        }
        CutSceneText.SetActive(false); // Ensure the cutscene text is initially inactive

        if (shieldBar == null)
        {
            shieldBar = GameObject.Find("boss-shieldBar").GetComponent<Slider>();
        }

        if (!OniSpriteRenderer)
        {
            OniSpriteRenderer = GetComponent<SpriteRenderer>();
        }
        _bossPosOffset = new Vector2(0f, -9f); // Offset to adjust the bullet spawn position

        if (!_bossHealthManager)
        {
            _bossHealthManager = GetComponent<BossHealthManager>();
        }
        if (PlayerTransform == null)
        {
            PlayerTransform = GameObject.Find("Player").GetComponent<Transform>();
        }
        if (BulletSpawnerTransform == null)
        {
            BulletSpawnerTransform = GameObject.Find("BossBulletSpawner").GetComponent<Transform>();
        }
    }

    private void Update()
    {
        if(_bossHealthManager.Health < _bossHealthManager.EnragedMinimumHP && !_bossHealthManager.isEnraged)
        {
            if (_cutSceneCoroutine == null)
            {
                _cutSceneCoroutine = StartCoroutine(EnragedCutscene());
            }
            _bossHealthManager.isEnraged = true;
            Enraged(); // Call the method to handle enraged state
        }
        if (_bossHealthManager.Health <= 0 && _attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null; // Reset attack coroutine reference
        }

        if (_bossHealthManager.Shield <= 0 && !_isStunned && _stunCoroutine == null)
        {
            _stunCoroutine = StartCoroutine(StunEnemy(StunDuration));
        }


        if (!_isStunned)
        {
            FlipAssetX();

            float distanceBetweenEntity = Vector2.Distance(transform.position - (Vector3)_bossPosOffset, PlayerTransform.position);
            _playerPos = PlayerTransform.position;

            if (distanceBetweenEntity > MaxChaseDistance && !IsOnCutscene)
            {
                ChasePlayer();
                if (_attackCoroutine != null)
                {
                    StopCoroutine(_attackCoroutine);
                    _attackCoroutine = null;
                }
            }
            else if (!_onCooldown && !IsOnCutscene)
            {
                _audioPlayed = false; // Reset to allow sound to play again
                _attackCoroutine = StartCoroutine(StartAttack());
                _onCooldown = true;
            }

            if (_onCooldown)
            {
                ElapsedTime += Time.deltaTime;
                if (ElapsedTime >= AttackCooldown)
                {
                    _onCooldown = false;
                    ElapsedTime = 0f;
                }
            }
        }
        else
        {
            // Optional: Add logic to handle the Oni's behavior while stunned, e.g., stop movement, play animation, etc.
            Debug.Log("Oni is currently stunned and cannot move or attack.");
        }
    }

    void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, _playerPos, BossSpeed * Time.deltaTime);


        if(_audioPlayed == false)
        {
            AudioManager.PlaySfx(AudioManager.FirstBossMoveSfx);
            _audioPlayed = true;
        }
        _elapsedTime += Time.deltaTime;
        if(_elapsedTime >= 3.05f)
        {
            AudioManager.PlaySfx(AudioManager.FirstBossMoveSfx);
            _elapsedTime = 0f; // Reset elapsed time after playing the sound
        }
    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(0.3f);

        GameObject enemyBullet = Instantiate(EnemyBullet, BulletSpawnerTransform.position, BulletSpawnerTransform.rotation);
        AudioManager.PlaySfx(AudioManager.PlayerAttackSfx);

        yield return null;
    }

    void FlipAssetX()
    {
        if(_playerPos.x > transform.position.x)
        {
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
        else if (_playerPos.x < transform.position.x)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }

    IEnumerator StunEnemy(float stunDuration)
    {
        _isStunned = true;
        AudioManager.PlaySfx(AudioManager.ShieldBreakSfx); // Play shield break sound effect

        yield return new WaitForSeconds(stunDuration);
        _isStunned = false;
        shieldBar.value = _bossHealthManager.Shield = _bossHealthManager.MaxShield; // Restore shield after stun
        Debug.Log("Oni is no longer stunned.");

        _stunCoroutine = null; // Reset stun coroutine reference
        _audioPlayed = false; // Reset hasPlayed to allow sound to play again
    }

    void Enraged()
    {
        _bossHealthManager.ShieldBar.maxValue = _bossHealthManager.Shield = _bossHealthManager.MaxShield = 50;
        OniSpriteRenderer.color = Color.red; // Change color to indicate enraged state
        MaxChaseDistance = 20f;
        BossSpeed = 3f;
    }

    IEnumerator EnragedCutscene()
    {
        yield return new WaitForSeconds(1f);

        IsOnCutscene = true;
        CutSceneBorder.SetActive(true);
        CutSceneText.SetActive(true);

        yield return new WaitForSeconds(5f); // Wait for the cutscene to play out

        CutSceneBorder.SetActive(false);
        CutSceneText.SetActive(false);

        _cutSceneCoroutine = null; // Reset cutscene coroutine reference
        IsOnCutscene = false; // Reset the cutscene flag
        yield return null;
    }
}
