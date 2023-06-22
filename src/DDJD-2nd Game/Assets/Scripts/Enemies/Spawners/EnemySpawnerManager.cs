using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyBox;
using System;

public enum SpawnerType
{
    Normal,
    Unlimitted,
}

public class EnemySpawnerManager : MonoBehaviour
{
    [SerializeField]
    private EnemySpawner _enemySpawner;

    [SerializeField]
    private List<EnemySpawnerComponent> _spawners;

    [SerializeField]
    private SpawnerType _type;

    [SerializeField]
    private float _spawnTimer = 2.0f;

    private Coroutine _spawnCoroutine;

    [ConditionalField(nameof(_type), false, SpawnerType.Normal)]
    [SerializeField]
    private int _totalAmountOfEnemies = 10;

    private int _aliveEnemies;

    private int _remainingEnemies;

    private List<GameObject> _spawnedEnemies = new List<GameObject>();

    public Action<BasicEnemy> OnSpawn;

    private bool _attackPlayer = false;

    private BoxCollider _collider;

    public float _spawnRadius;

    public void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _spawnRadius = _collider.size.x / 2.0f + _collider.size.z / 2.0f;
    }

    public void Start()
    {
        ResetSpawner();
        foreach (EnemySpawnerComponent spawner in _spawners)
        {
            spawner.SetEnemySpawnerManager(this);
        }
    }

    public void EnemyDied(BasicEnemy enemy)
    {
        _aliveEnemies--;

        if (_aliveEnemies <= 0)
        {
            //Inform mission manager
            MissionController.Instance.CompleteFightGoal(_enemySpawner);
        }
    }

    public float SpawnRadius
    {
        get { return _spawnRadius; }
    }

    private BasicEnemy SpawnEnemy()
    {
        EnemySpawnerComponent spawner;
        bool canSpawn = false;
        BasicEnemy enemy = null;
        //TODO:: Could be more efficient
        spawner = _spawners[UnityEngine.Random.Range(0, _spawners.Count)];
        EnemyInfo enemyInfo = _enemySpawner.EnemySpawnerInfo.GetRandomEnemyInfo();
        enemy = spawner.SpawnEnemy(enemyInfo);
        if (enemy != null)
        {
            _spawnedEnemies.Add(enemy.gameObject);
            OnSpawn?.Invoke(enemy);
        }
        return enemy;
    }

    public void StartSpawn(bool attackPlayer)
    {
        _attackPlayer = attackPlayer;
        _spawnCoroutine = StartCoroutine(SpawnCoroutine());
    }

    public void ResetSpawner()
    {
        _remainingEnemies = _totalAmountOfEnemies;
        _aliveEnemies = _totalAmountOfEnemies;
    }

    public void StopSpawn()
    {
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
        }
    }

    private IEnumerator SpawnCoroutine()
    {
        while (_remainingEnemies > 0 || _type == SpawnerType.Unlimitted)
        {
            BasicEnemy enemy = SpawnEnemy();
            if (_remainingEnemies > 0 && enemy != null)
            {
                _remainingEnemies--;
            }
            yield return new WaitForSeconds(_spawnTimer + UnityEngine.Random.Range(0.0f, 0.5f));
        }
    }

    public int TotalAmountOfEnemies
    {
        get { return _totalAmountOfEnemies; }
        set { _totalAmountOfEnemies = value; }
    }

    public List<GameObject> SpawnedEnemies
    {
        get { return _spawnedEnemies; }
    }
}
