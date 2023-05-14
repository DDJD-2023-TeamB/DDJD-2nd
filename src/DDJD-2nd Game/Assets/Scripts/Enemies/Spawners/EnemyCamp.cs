using UnityEngine;
using System.Collections;
using MyBox;

public enum SpawnerTriggerType
{
    OnProximity,
    OnCombat
}

public class EnemyCamp : MonoBehaviour
{
    private EnemySpawnerManager _spawnerManager;
    private EnemyCommunicator _enemyCommunicator;
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
        _enemyCommunicator = GetComponent<EnemyCommunicator>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _enemyCommunicator.SetMessageAction(
            typeof(PlayerSightedMessage),
            (EnemyMessage msg) => _playerSighted = true
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        if (other.CompareTag("Player"))
        {
            Debug.Log("yey");
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
}
