using UnityEngine;

[CreateAssetMenu(
    fileName = "EnemySpawner",
    menuName = "Scriptable Objects/Enemies/Spawner",
    order = 2
)]
public class EnemySpawner : ScriptableObject
{
    [SerializeField]
    private EnemySpawnerInfo _enemySpawnerInfo;

    [SerializeField]
    private bool _completed = false;

    public bool Completed
    {
        get => _completed;
        set => _completed = value;
    }
    public EnemySpawnerInfo EnemySpawnerInfo
    {
        get { return _enemySpawnerInfo; }
    }
}
