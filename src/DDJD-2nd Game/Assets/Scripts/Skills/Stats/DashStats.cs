using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashStats : SkillStats
{
    [SerializeField]
    private float _speed;
    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }

    public DashStats(float damage, float cooldown, float speed)
        : base(damage, cooldown)
    {
        _speed = speed;
    }
}
