using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DashStats : SkillStats
{
    [SerializeField]
    private float _speed;
    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }

    [SerializeField]
    private float _distance;
    public float Distance
    {
        get => _distance;
        set => _distance = value;
    }

    public DashStats(float damage, float cooldown, float speed)
        : base(damage, cooldown)
    {
        _speed = speed;
    }
}
