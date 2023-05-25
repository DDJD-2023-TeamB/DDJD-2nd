using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "EnemyInfo",
    menuName = "Scriptable Objects/Enemies/EnemyInfo",
    order = 2
)]
public class EnemyInfo : ScriptableObject
{
    [SerializeField]
    private EnemySkills _enemySkills;

    [SerializeField]
    private GameObject _prefab;

    public EnemySkills EnemySkills
    {
        get { return _enemySkills; }
    }

    public GameObject Prefab
    {
        get { return _prefab; }
    }
}
