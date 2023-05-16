using UnityEngine;
using System;

public class NoiseListener : MonoBehaviour
{
    [SerializeField, Tooltip("The range at which the enemy can hear the player")]
    private float _hearingRange = 1.0f;

    public Action<Vector3> OnNoiseHeard;

    private BasicEnemy _enemy;

    public void Awake()
    {
        _enemy = GetComponent<BasicEnemy>();
    }

    public void HearNoise(Vector3 noisePosition)
    {
        if (OnNoiseHeard != null)
        {
            OnNoiseHeard(noisePosition);
        }
    }

    public bool CanHearPlayer()
    {
        return Vector3.Distance(_enemy.Player.transform.position, transform.position)
            <= _hearingRange;
    }
}
