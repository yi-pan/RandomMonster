using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;

    private float _minSpawnTime;
    private float _maxSpawnTime;
    private float _spawnTime;

    private float _enemyCount;
    private int _maxEnemyCount;
    private float _spawnDistance;

    private GameController _gameController;
    private Vector2 _spawnPosition;
    private Camera _camera;

    private void Start()
    {
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();
        _minSpawnTime = _gameController.enemyAMinSpawnTime;
        _maxSpawnTime = _gameController.enemyAMaxSpawnTime;
        _maxEnemyCount = _gameController.enemyAMaxCount;
        _spawnDistance = _gameController.enemyASpawnDistance;
        _camera = Camera.main;
    }
    private void Awake()
    {
        _enemyCount = 0;
        SetSpawnTime();
    }

    public UnityEvent OnEnemiesDie;
    private void Update()
    {
        if (_enemyCount == _maxEnemyCount && FindObjectOfType<EnemyMovement>()==null)
        {
            OnEnemiesDie.Invoke();
        }
        _spawnTime -= Time.deltaTime;
        if(_spawnTime <= 0 && _enemyCount < _maxEnemyCount)
        {
            _spawnPosition = GetRandomOffScreenPosition();
            Instantiate(_enemyPrefab, _spawnPosition, Quaternion.identity);
            _enemyCount++;
            SetSpawnTime();
        }
    }

    private Vector2 GetRandomOffScreenPosition()
    {
        // Get the screen bounds in world space
        Vector2 screenMin = _camera.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 screenMax = _camera.ViewportToWorldPoint(new Vector2(1, 1));

        // Randomly choose if the enemy spawns on the top, bottom, left, or right
        int side = Random.Range(0, 4);

        Vector2 spawnPosition = Vector2.zero;

        switch (side)
        {
            case 0: // Top
                spawnPosition = new Vector2(Random.Range(screenMin.x, screenMax.x), screenMax.y + _spawnDistance);
                break;
            case 1: // Bottom
                spawnPosition = new Vector2(Random.Range(screenMin.x, screenMax.x), screenMin.y - _spawnDistance);
                break;
            case 2: // Left
                spawnPosition = new Vector2(screenMin.x - _spawnDistance, Random.Range(screenMin.y, screenMax.y));
                break;
            case 3: // Right
                spawnPosition = new Vector2(screenMax.x + _spawnDistance, Random.Range(screenMin.y, screenMax.y));
                break;
        }

        return spawnPosition;
    }
    private void SetSpawnTime()
    {
        _spawnTime = Random.Range(_minSpawnTime, _maxSpawnTime);
    }
}
