using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UnknownEnemyBehaviorscript : MonoBehaviour
{
    public Vector2 MinTeleportRange;
    public Vector2 MaxTeleportRange;

    public Sprite BarrageBulletSprite;
    public GameObject BarrageBulletPrefab; // Prefab for the barrage bullet
    public GameObject AcceptOrRejectGroup;

    public SpriteRenderer SpriteRenderer;
    public SpriteRenderer[] BarrageSpritePlaceholder;
    public Transform[] BarrageAttackSpawnPos;

    public float IntervalBetweenTeleports = 10f;
    public float AttackCooldown = 3f;
    public int BarrageCount = 1;
    private Coroutine _teleportCoroutine;
    private Coroutine _fadeCoroutine;
    private Coroutine _barrageCoroutine;
    private Coroutine _stunCoroutine; // Coroutine for handling stun effect

    public float StunDuration = 5f; // Duration of the stun effect
    private bool _isStunned = false; // Flag to check if the enemy is stunned
    private  bool _isFading = false; // Flag to check if the enemy is currently fading
    private bool _isEnraged = false; // Flag to check if the enemy is enraged

    public float ElapsedTime = 0f;
    public float FadeDuration = 1f;
    private bool _fadeOut = true; // Set to true for fade out, false for fade in

    private BossHealthManager _bossHealthManager;
    private Slider _bossShieldBar;

    private AudioManagerScript _audioManager;
    private bool _lastShieldQueue = false; // Track the last shield state to avoid unnecessary updates

    private AcceptOrRejectScript _acceptRejectScript; // Reference to the AcceptOrRejectScript for handling player decisions
    public LevelTwoDialogueScript LevelTwoDialogueScript; // Reference to the LevelTwoDialogueScript for handling dialogues

    private Animator _animator; // Reference to the Animator for handling animations

    private void Start()
    {
        if (!_animator)
        {
            _animator = GetComponent<Animator>();
        }
        if (!_audioManager && GameObject.Find("AudioManager") != null)
        {
            _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManagerScript>();
        }
        if (!_bossHealthManager)
        {
            _bossHealthManager = GameObject.FindGameObjectWithTag("Enemy_boss").GetComponent<BossHealthManager>();
        }
        if(!_bossShieldBar)
        {
            _bossShieldBar = GameObject.Find("boss-shieldBar").GetComponent<Slider>();
        }

        if (!SpriteRenderer)
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        _teleportCoroutine = StartCoroutine(TeleportRandomly());
    }

    void Update()
    {
        if(_isStunned)
        {
            if (_fadeCoroutine == null && SpriteRenderer.color.a < 1f)
            {
                _fadeOut = true; // Set to true for fade out before stun
                _fadeCoroutine = StartCoroutine(FadeSprite()); // Start fading in immediately
            }
            return; // Skip the rest of the update if stunned
        }

        if (!_isStunned && _barrageCoroutine == null)
        {
            _barrageCoroutine = StartCoroutine(BarrageAttack());
        }

        if(_bossHealthManager.Health <= _bossHealthManager.EnragedMinimumHP && !_bossHealthManager.isEnraged)
        {
            Debug.Log("Boss is enraged!");
            _bossHealthManager.isEnraged = true;
            Enraged(); // Call the method to handle enraged state
        }

        if (_bossHealthManager.Shield <= 0 && !_isStunned && _stunCoroutine == null && !_isEnraged)
        {
            _stunCoroutine = StartCoroutine(StunEnemy(StunDuration));
        }
        
        if (_bossHealthManager.Health <= 10f)
        {
            _animator.SetBool("isEnraged", false); // Trigger enraged animations when health is low
            _stunCoroutine = StartCoroutine(StunEnragedEnemy());
        }
    }

    IEnumerator TeleportRandomly()
    {
        while (true)
        {
            // Wait for the specified interval before teleporting
            yield return new WaitForSeconds(IntervalBetweenTeleports);
            // Generate a random position within the defined ranges
            float randomX = Random.Range(MinTeleportRange.x, MaxTeleportRange.x);
            float randomY = Random.Range(MinTeleportRange.y, MaxTeleportRange.y);
            Vector2 randomPosition = new Vector2(randomX, randomY);

            _isFading = true; // Set to true to indicate fading is in progress
            _fadeOut = true; // Set to true for fade out before teleporting
            _fadeCoroutine = StartCoroutine(FadeSprite());

            while (_fadeCoroutine != null)
            {
                // Wait until the fade coroutine is finished before teleporting
                yield return null;
            }

            // Teleport the enemy to the new position
            transform.position = randomPosition;

            yield return new WaitForSeconds(0.5f);

            _fadeOut = false; // Set to false for fade in after teleporting
            _fadeCoroutine = StartCoroutine(FadeSprite());
            _isFading = false; // Reset fading flag
        }
    }

    IEnumerator StunEnemy(float stunDuration)
    {
        _isStunned = true;

        if (_barrageCoroutine != null)
        {
            StopCoroutine(_barrageCoroutine);
            _barrageCoroutine = null; // Reset barrage coroutine reference
            foreach (SpriteRenderer sr in BarrageSpritePlaceholder)
            {
                sr.sprite = null; // Clear barrage sprites when stunned
            }
        }
        if(_teleportCoroutine != null)
        {
            StopCoroutine(_teleportCoroutine);
            if(SpriteRenderer.color.a < 1f)
            {
                _fadeOut = true; // Set to true for fade out before stun
                _fadeCoroutine = StartCoroutine(FadeSprite()); // Start fading in immediately
            }
            _teleportCoroutine = null; // Reset teleport coroutine reference
        }
        _audioManager.PlaySfx(_audioManager.ShieldBreakSfx); // Play shield break sound effect

        yield return new WaitForSeconds(stunDuration);
        _isStunned = false;
        _teleportCoroutine = StartCoroutine(TeleportRandomly()); // Restart teleport coroutine after stun
        _bossShieldBar.value = _bossHealthManager.Shield = _bossHealthManager.MaxShield; // Restore shield after stun
        Debug.Log("Oni is no longer stunned.");

        _stunCoroutine = null; // Reset stun coroutine reference
    }

    IEnumerator StunEnragedEnemy()
    {
        _isStunned = true; // Set the stunned flag to true

        if (_barrageCoroutine != null)
        {
            StopCoroutine(_barrageCoroutine);
            _barrageCoroutine = null; // Reset barrage coroutine reference
            foreach (SpriteRenderer sr in BarrageSpritePlaceholder)
            {
                sr.sprite = null; // Clear barrage sprites when stunned
            }
        }
        if (_teleportCoroutine != null)
        {
            StopCoroutine(_teleportCoroutine);
            _teleportCoroutine = null; // Reset teleport coroutine reference
        }
        if (_fadeCoroutine != null)
        {
            StopCoroutine(_fadeCoroutine);
            _fadeCoroutine = null; // Reset fade coroutine reference
            _fadeOut = true; // Set to true for fade out before stun
            _fadeCoroutine = StartCoroutine(FadeSprite()); // Start fading in immediately
        }
        transform.position = new Vector2(0, 11.75f); // Move the enemy to a specific position when stunned
        if(!_lastShieldQueue)
        {
            _audioManager.PlaySfx(_audioManager.ShieldBreakSfx); // Play shield break sound effect
        }
        _lastShieldQueue = true; // Set the last shield state to true to avoid unnecessary updates
        Collider2D col = GameObject.Find("unknown-collider").GetComponent<Collider2D>();
        col.enabled = false;

        if(!_acceptRejectScript)
        {
            _acceptRejectScript = GameObject.Find("Player").GetComponent<AcceptOrRejectScript>();
        }

        _acceptRejectScript.IsDeciding = true; // Set the deciding flag to true to allow player to accept or reject

        while (_acceptRejectScript.IsDeciding)
        {
            if (Vector2.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < 5f)
            {
                AcceptOrRejectGroup.SetActive(true); // Show the accept or reject UI group
                LevelTwoDialogueScript.OnAOR(); // Trigger the accept or reject dialogue
            }
            else
            {
                LevelTwoDialogueScript.OffAOR(); // Turn off the accept or reject dialogue if player is too far
                AcceptOrRejectGroup.SetActive(false); // Hide the accept or reject UI group if player is too far
            }

            if(Input.GetKey(KeyCode.Q))
            {
                _acceptRejectScript.IsDeciding = false; // Set the deciding flag to false to stop accepting or rejecting
                _acceptRejectScript.IsAccepted = true; // Set the accepted flag to true
            }
            else if (Input.GetKey(KeyCode.E))
            {
                _acceptRejectScript.IsDeciding = false; // Set the deciding flag to false to stop accepting or rejecting
                _acceptRejectScript.IsAccepted = false; // Set the accepted flag to false
            }

                Debug.Log("is deciding");
            yield return null; // Wait for the next frame while the player is deciding
        }

        AcceptOrRejectGroup.SetActive(false);
        _acceptRejectScript.IsDeciding = false; // Set the deciding flag to false to stop accepting or rejecting

        yield return null;
        _isStunned = false; // Reset the stunned flag
        col.enabled = true; // Re-enable the collider after the stun effect
        _stunCoroutine = null; // Reset stun coroutine reference

        LevelManagerScript levelManager = GameObject.Find("GameManager").GetComponent<LevelManagerScript>();

        if (!_acceptRejectScript.IsDeciding && _acceptRejectScript.IsAccepted)
        {
            //Destroy(gameObject); // Destroy the enemy if the player accepts the cutscene
            SceneManager.LoadScene("You-Win-Cutscene");
        }
        if (!_acceptRejectScript.IsDeciding && !_acceptRejectScript.IsAccepted)
        {
            //Destroy(gameObject); // Destroy the enemy if the player accepts the cutscene
            //levelManager.RejectCutscene();
            SceneManager.LoadScene("You-Lose-Cutscene"); // Load the lose cutscene if the player rejects
        }
    }

    IEnumerator FadeSprite()
    {
        float elapsed = 0f;
        Color startColor = SpriteRenderer.color;
        float startAlpha = startColor.a;
        float endAlpha = _fadeOut ? 0f : 1f;

        while (elapsed < FadeDuration)
        {
            elapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / FadeDuration);
            SpriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);
            yield return null;
        }

        // Ensure final alpha is set
        SpriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, endAlpha);
        _fadeCoroutine = null; // Reset fade coroutine reference
    }

    IEnumerator BarrageAttack()
    {
        yield return new WaitForSeconds(AttackCooldown); // Initial delay before starting the barrage attack

        foreach (SpriteRenderer sr in BarrageSpritePlaceholder)
        {
            // Set the sprite for the barrage attack
            sr.sprite = BarrageBulletSprite;
            // Wait for a short duration before moving to the next sprite
            if(_isEnraged == false)
            {
                yield return new WaitForSeconds(0.7f);
            }
        }

        while(_isFading)
        {
            // Wait until the fade coroutine is finished before proceeding with the barrage attack
            yield return null;
        }


        for(int i=0; i < BarrageCount; i++)
        {
            int index = 0;

            foreach (Transform spawnPos in BarrageAttackSpawnPos)
            {
                // Instantiate the barrage attack at the spawn position
                GameObject bullet = Instantiate(BarrageBulletPrefab, spawnPos.position, spawnPos.rotation);
                _audioManager.PlaySfx(_audioManager.FirstBossAttackSfx); // Play barrage attack sound
                BarrageSpritePlaceholder[index].sprite = null;
                index++;
            }
            yield return new WaitForSeconds(0.5f); // Wait before the next barrage attack
        }

        yield return null;
        _barrageCoroutine = null; // Reset barrage coroutine reference
    }

    void Enraged()
    {
        _animator.SetBool("isEnraged", true); // Set the animator parameter to trigger enraged animations
        IntervalBetweenTeleports = 5f; // Decrease teleport interval when enraged
        AttackCooldown = 2f; // Decrease attack cooldown when enraged
        _isEnraged = true; // Set the enraged flag to true
        BarrageCount = 2; // Increase barrage count when enraged
    }

}
