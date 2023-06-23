using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using FMODUnity;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Player _player;

    public Action OnEnemyKilled;

    public Action OnCombatStart;
    public Action OnCombatEnd;

    private Coroutine _combatCoroutine;
    private List<BasicEnemy> _enemiesAggroed = new List<BasicEnemy>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }
    }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Music Volume", 1.0f, false);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("SFX Volume ", 1.0f, false);
    }

    private void CombatStart()
    {
        OnCombatStart?.Invoke();
        _combatCoroutine = StartCoroutine(CombatUpdate());
    }

    public void CombatEnd()
    {
        OnCombatEnd?.Invoke();
        StopCoroutine(_combatCoroutine);
    }

    public void EnemyDied(BasicEnemy enemy)
    {
        _enemiesAggroed.Remove(enemy);

        Vector3 position = enemy.transform.position;
        position.y = 0;

        enemy.EnemyCommunicator.SendMessageToEnemies(
            new PlayerSightedMessage(_player.transform.position)
        );

        if (_enemiesAggroed.Count <= 0)
        {
            CombatEnd();
        }
        OnEnemyKilled?.Invoke();
    }

    public void PlayerLostOfSight(BasicEnemy enemy)
    {
        if (_enemiesAggroed.Contains(enemy))
        {
            _enemiesAggroed.Remove(enemy);
        }
        if (_enemiesAggroed.Count <= 0)
        {
            CombatEnd();
        }
    }

    public void PlayerDetected(BasicEnemy enemy)
    {
        if (_enemiesAggroed.Contains(enemy))
        {
            return;
        }
        _enemiesAggroed.Add(enemy);
        if (_enemiesAggroed.Count == 1)
        {
            CombatStart();
        }
    }

    // To ensure combat doesn't stay forever in case of bug
    private IEnumerator CombatUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(5.0f);
            List<BasicEnemy> enemiesToRemove = new List<BasicEnemy>();
            foreach (BasicEnemy enemy in _enemiesAggroed)
            {
                if (enemy == null)
                {
                    enemiesToRemove.Add(enemy);
                }
            }
            foreach (BasicEnemy enemy in enemiesToRemove)
            {
                _enemiesAggroed.Remove(enemy);
            }
            if (_enemiesAggroed.Count <= 0)
            {
                CombatEnd();
            }
        }
    }

    public Player Player
    {
        get { return _player; }
    }
}
