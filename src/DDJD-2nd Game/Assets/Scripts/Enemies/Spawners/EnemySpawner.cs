using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private float _spawnDelay;

    [SerializeField]
    private float _spawnRadius;

    private bool _canSpawn;

    private void Awake()
    {
        _canSpawn = true;
    }

    private void Update() { }

    public GameObject SpawnEnemy(EnemyInfo info)
    {
        Vector3 position;
        UnityEngine.AI.NavMeshHit hit;
        int tries = 10;
        do
        {
            position = transform.position + Random.insideUnitSphere * _spawnRadius;
            tries--;
            if (tries == 0)
            {
                Debug.LogError("Could not find a position to spawn enemy at " + transform.position);
                return null;
            }
        } while (
            UnityEngine.AI.NavMesh.SamplePosition(
                position,
                out hit,
                1.0f,
                UnityEngine.AI.NavMesh.AllAreas
            ) == false
        );
        position = hit.position;
        GameObject enemy = Instantiate(info.Prefab, position, Quaternion.identity);

        BasicEnemy basicEnemy = enemy.GetComponent<BasicEnemy>();
        basicEnemy.EnemySkills = info.EnemySkills;
        StartCoroutine(SpawnCooldown());
        return enemy;
    }

    private IEnumerator SpawnCooldown()
    {
        _canSpawn = false;
        yield return new WaitForSeconds(_spawnDelay);
        _canSpawn = true;
    }

    public bool CanSpawn()
    {
        return _canSpawn;
    }
}
