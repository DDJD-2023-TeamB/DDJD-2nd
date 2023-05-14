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
    private EnemySpawnerInfo _enemySpawnerInfo;

    [SerializeField]
    private List<EnemySpawner> _spawners;

    [SerializeField]
    private SpawnerType _type;

    [SerializeField]
    private float _spawnTimer = 2.0f;

    private Coroutine _spawnCoroutine;

    [ConditionalField(nameof(_type), false, SpawnerType.Normal)]
    [SerializeField]
    private int _totalAmountOfEnemies = 10;

    private int _remainingEnemies;

    public void Awake() { }

    public void Start() { }

    private void SpawnEnemy()
    {
        EnemySpawner spawner;
        bool spawned = false;
        do
        {
            //TODO:: Could be more efficient
            spawner = _spawners[Random.Range(0, _spawners.Count)];
            spawned = spawner.CanSpawn();
            EnemyInfo enemyInfo = _enemySpawnerInfo.GetRandomEnemyInfo();
            spawner.SpawnEnemy(enemyInfo);
        } while (!spawned);
    }

    public void StartSpawn()
    {
        Debug.Log("Start spawn");
        _remainingEnemies = _totalAmountOfEnemies;
        _spawnCoroutine = StartCoroutine(SpawnCoroutine());
    }

    public void StopSpawn()
    {
        Debug.Log("Stop spawn");
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
            Debug.Log("remaining enemies: " + _remainingEnemies);
            if (_remainingEnemies > 0)
            {
                _remainingEnemies--;
            }
            Debug.Log("remaining enemies: " + _remainingEnemies);
            yield return new WaitForSeconds(_spawnTimer + Random.Range(0.0f, 0.5f));
        }
    }

    public int TotalAmountOfEnemies
    {
        get { return _totalAmountOfEnemies; }
        set { _totalAmountOfEnemies = value; }
    }
}
