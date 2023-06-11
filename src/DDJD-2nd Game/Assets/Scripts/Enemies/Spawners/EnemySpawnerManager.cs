using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyBox;

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

    public void Awake() { }

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
        _spawnedEnemies.Remove(enemy.gameObject);
        _aliveEnemies--;

        if (_aliveEnemies <= 0)
        {
            //Inform mission manager
            MissionController.Instance.CompleteFightGoal(_enemySpawner);
        }
    }

    private void SpawnEnemy()
    {
        EnemySpawnerComponent spawner;
        bool spawned = false;
        GameObject enemy = null;
        do
        {
            //TODO:: Could be more efficient
            spawner = _spawners[Random.Range(0, _spawners.Count)];
            spawned = spawner.CanSpawn();
            EnemyInfo enemyInfo = _enemySpawner.EnemySpawnerInfo.GetRandomEnemyInfo();
            enemy = spawner.SpawnEnemy(enemyInfo);
        } while (!spawned);
        if (enemy != null)
        {
            _spawnedEnemies.Add(enemy);
        }
    }

    public void StartSpawn()
    {
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
            SpawnEnemy();
            if (_remainingEnemies > 0)
            {
                _remainingEnemies--;
            }
            yield return new WaitForSeconds(_spawnTimer + Random.Range(0.0f, 0.5f));
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
