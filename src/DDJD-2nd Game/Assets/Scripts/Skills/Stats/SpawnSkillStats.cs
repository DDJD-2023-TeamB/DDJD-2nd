using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnSkillStats : SkillStats
{
    [SerializeField]
    private float _duration;
    public float Duration
    {
        get => _duration;
        set => _duration = value;
    }

    [SerializeField]
    private float _damageRadius;
    public float DamageRadius
    {
        get => _damageRadius;
        set => _damageRadius = value;
    }

    public SpawnSkillStats(float damage, float cooldown, float duration)
        : base(damage, cooldown)
    {
        _duration = duration;
    }
}
