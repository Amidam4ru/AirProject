using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyPool))]
public class EnemySpawner : MonoBehaviour
{
    [Header("Spawners")]
    [SerializeField] private List<SpawnPointsCircle> _spawnPointsCircles;
    [SerializeField] private float _maxSpawnTime;
    [SerializeField] private float _minSpawnTime;
    [SerializeField] private float _spawnTimeDuration;
    [SerializeField] private float _updateSpawnTimeTime;
    [SerializeField] private TerritoryChanging _territoryChanging;
    [SerializeField] private float _upgradeTime;

    private int _enemyCounter;
    private float _spawnTimer;
    private float _spawnTime;
    private List<SpawnPoint> _spawnPoints;
    private float _updateSpawnTimeTimer;
    private float _upgradeTimer;
    private int _maxEnemyLevel;
    private EnemyPool _pool;

    private void Awake()
    {
        _enemyCounter = 1;
        _spawnTimer = 0;
        _spawnTime = _maxSpawnTime;
        _updateSpawnTimeTimer = 0;
        _spawnPoints = new List<SpawnPoint>();
        _upgradeTimer = 0;
        _maxEnemyLevel = 0;
        _pool = GetComponent<EnemyPool>();
    }

    private void Start()
    {
        for (int i = 0; i < _territoryChanging.Level; i++)
        {
            _spawnPoints.AddRange(_spawnPointsCircles[i].GetSpawnPoints());
        }

        StartCoroutine(UpgradeSpawner());
    }

    private void Update()
    {
        UpdateSpawnTime();
        SpawnTimeCalculate();
    }

    private void SpawnTimeCalculate()
    {
        _spawnTimer += Time.deltaTime;

        if (_spawnTimer >= _spawnTime)
        {
            _spawnTimer = 0;

            for (int i = 0; i < _enemyCounter; i++)
            {;
                Spawn();
            }
        }
    }

    private void UpdateSpawnTime()
    {
        _updateSpawnTimeTimer += Time.deltaTime;

        if (_updateSpawnTimeTimer >= _updateSpawnTimeTime)
        {
            _updateSpawnTimeTimer = 0;
            _spawnTime -= _spawnTimeDuration;

            if (_spawnTime < _minSpawnTime)
            {
                _spawnTime = _maxSpawnTime;
                _enemyCounter++;
            }
        }

    }

    private IEnumerator UpgradeSpawner()
    {
        while (_maxEnemyLevel < _pool.EnemyLevelCount)
        {
            _upgradeTimer += Time.deltaTime;

            if (_upgradeTimer >= _upgradeTime)
            {
                _maxEnemyLevel++;
                _upgradeTimer = 0;
            }

            yield return null;
        }
    }

    private void Spawn()
    {
        int levelEnemy = Random.Range(0, _maxEnemyLevel);

        _pool.GetEnemy(levelEnemy, _spawnPoints[Random.Range(0, _spawnPoints.Count)]);
    }
}
