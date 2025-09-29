using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    public float BulletForce = 40f;

    private Transform _playerTransform;
    private Rigidbody2D _rb2D;

    [SerializeField] 
    private GameObject _explosionEffect;

    private Vector2 _playerPos;

    private BossHealthManager _bossHealthManager;

    private void Start()
    {
        if (!_bossHealthManager)
        {
            _bossHealthManager = GameObject.FindGameObjectWithTag("Enemy_boss").GetComponent<BossHealthManager>();
        }

        _playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        _playerPos = _playerTransform.position;
        _rb2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if(_bossHealthManager.isEnraged)
        {
            BulletForce = 50f; // Increase bullet speed when the boss is enraged
        }

        Vector2 direction = (_playerPos - (Vector2)transform.position).normalized;
        Vector3 rotation = transform.position - (Vector3)_playerPos;

        //gets random pos around playerPos for inaccuracy in attack to make it seem natural
        float randomX = Random.Range(_playerPos.x - 0.2f, _playerPos.x + 0.2f);
        float randomY = Random.Range(_playerPos.y - 0.2f, _playerPos.y + 0.2f);
        _playerPos = new Vector2(randomX, randomY);

        transform.position = Vector2.MoveTowards(transform.position, _playerPos, BulletForce * Time.deltaTime);

        float angle = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); // Adjust rotation to face the player position


        if (Vector2.Distance(transform.position, _playerPos) < 0.1f)
        {
            ExplodeBullet();
        }
    }

    void ExplodeBullet()
    {
        GameObject explosion = Instantiate(_explosionEffect, transform.position + new Vector3(0f, -4f, 0f), Quaternion.identity);
        Destroy(gameObject);
        Destroy(explosion, 1.61f); // Destroy the explosion effect after 1 second
    }
}
