using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FightGoal : GoalObject
{
    [SerializeField]
    private EnemySpawner _enemySpawner;

    public EnemySpawner EnemySpawner
    {
        get { return _enemySpawner; }
    }
}
