using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using MyBox;

public enum SpawnerTriggerType
{
    OnProximity,
    OnCombat
}

public class EnemyCamp : MonoBehaviour, NonCollidable
{
    private EnemySpawnerManager _spawnerManager;

    [SerializeField]
    private EnemyCommunicator _enemyCommunicator;

    [SerializeField]
    private List<GameObject> _defaultEnemies = new List<GameObject>();

    //Default enemies instantiated when player enters camp
    private List<GameObject> _copiedEnemies = new List<GameObject>();
    private Player _player;

    [SerializeField]
    [Tooltip("Can be reseted by leaving the camp and coming back")]
    private float _isResetable;

    [SerializeField]
    private SpawnerTriggerType _triggerType;

    [ConditionalField(nameof(_triggerType), false, SpawnerTriggerType.OnProximity)]
    [SerializeField]
    private float _triggerRadius;

    private Coroutine _coroutine;

    private bool _playerSighted = false;

    private void Awake()
    {
        _spawnerManager = GetComponent<EnemySpawnerManager>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _enemyCommunicator.SetMessageAction(
            typeof(PlayerSightedMessage),
            (EnemyMessage msg) => _playerSighted = true
        );

        //Disable default enemies
        foreach (GameObject enemy in _defaultEnemies)
        {
            enemy.SetActive(false);
        }
    }

    private void InstantiateDefaultEnemies()
    {
        List<GameObject> enemies = new List<GameObject>();
        foreach (GameObject enemy in _defaultEnemies)
        {
            GameObject instantiatedEnemy = Instantiate(
                enemy,
                enemy.transform.position,
                enemy.transform.rotation,
                enemy.transform.parent
            );
            instantiatedEnemy.SetActive(true);
            enemies.Add(instantiatedEnemy);
        }

        _copiedEnemies = enemies;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _coroutine = StartCoroutine(CampCoroutine());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _spawnerManager.StopSpawn();
            }
        }

        //Move enemies back to camp
        List<GameObject> enemies = new List<GameObject>(_copiedEnemies);

        enemies.AddRange(_spawnerManager.SpawnedEnemies);
        foreach (GameObject enemy in enemies)
        {
            // Get random position in camp
            Vector3 randomPosition = new Vector3(
                Random.Range(-5.0f, 5.0f),
                0.0f,
                Random.Range(-5.0f, 5.0f)
            );
            randomPosition += transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomPosition, out hit, 5.0f, UnityEngine.AI.NavMesh.AllAreas);
            //Draw ray to see where the enemy is going

            if (enemy != null)
            {
                Debug.DrawRay(
                    enemy.transform.position,
                    hit.position - enemy.transform.position,
                    Color.red,
                    25.0f
                );
                _enemyCommunicator.SendMessage(enemy, new MoveToMessage(hit.position));
            }
        }
    }

    private IEnumerator CampCoroutine()
    {
        _playerSighted = false;
        bool continueCoroutine = true;
        while (continueCoroutine)
        {
            yield return new WaitForSeconds(1.0f);
            switch (_triggerType)
            {
                case SpawnerTriggerType.OnProximity:
                    if (
                        Vector3.Distance(transform.position, _player.transform.position)
                        < _triggerRadius
                    )
                    {
                        _spawnerManager.StartSpawn();
                        continueCoroutine = false;
                        break;
                    }
                    break;
                case SpawnerTriggerType.OnCombat:
                    if (_playerSighted)
                    {
                        _spawnerManager.StartSpawn();
                        continueCoroutine = false;
                        break;
                    }
                    break;
            }
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _triggerRadius);
    }

    public void OnDisable()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        foreach (GameObject enemy in _copiedEnemies)
        {
            Destroy(enemy);
        }

        _copiedEnemies.Clear();
    }

    public void OnEnable()
    {
        _spawnerManager.ResetSpawner();
        _playerSighted = false;
        InstantiateDefaultEnemies();
    }
}
