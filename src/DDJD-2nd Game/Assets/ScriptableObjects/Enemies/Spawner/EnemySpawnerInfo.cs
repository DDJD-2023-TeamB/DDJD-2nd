using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawner", menuName = "Scriptable Objects/Enemies/Spawner", order = 2)]
public class EnemySpawnerInfo : ScriptableObject
{
    [SerializeField]
    private List<EnemyInfo> _enemiesInfo;

    [SerializeField]
    private float _spawnerTimer;

    [SerializeField]
    public List<int> _frequencies;

    public EnemyInfo GetRandomEnemyInfo()
    {
        int randomIndex = Random.Range(0, _enemiesInfo.Count);
        // TODO:: Make frequency
        return _enemiesInfo[randomIndex];
    }

    public List<EnemyInfo> EnemiesInfo
    {
        get { return _enemiesInfo; }
    }
    public List<int> Frequencies
    {
        get { return _frequencies; }
    }

    public float SpawnerTimer
    {
        get { return _spawnerTimer; }
    }
}
