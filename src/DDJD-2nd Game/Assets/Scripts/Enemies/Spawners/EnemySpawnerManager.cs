using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawnerManager : MonoBehaviour
{
    [SerializeField]
    private EnemySpawnerInfo _enemySpawnerInfo;

    [SerializeField]
    private List<EnemySpawner> _spawners;

    [SerializeField]
    private float _spawnTimer = 2.0f;

    private Coroutine _spawnCoroutine;

    [SerializeField]
    private float _amountOfEnemies;

    public void Awake() { }

    public void Start()
    {
        StartSpawn();
    }

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
        _spawnCoroutine = StartCoroutine(SpawnCoroutine());
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
        while (_amountOfEnemies > 0 || _amountOfEnemies == -1)
        {
            SpawnEnemy();
            if (_amountOfEnemies > 0)
            {
                _amountOfEnemies--;
            }
            Debug.Log("Spawn enemy");
            yield return new WaitForSeconds(_spawnTimer + Random.Range(0.0f, 0.5f));
        }
    }

    public float AmountOfEnemies
    {
        get { return _amountOfEnemies; }
        set { _amountOfEnemies = value; }
    }
}
