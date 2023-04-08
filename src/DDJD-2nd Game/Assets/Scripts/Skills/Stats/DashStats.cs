using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DashStats : SkillStats
{
    [SerializeField]
    private float _force;
    public float Force
    {
        get => _force;
        set => _force = value;
    }

    [SerializeField]
    private float _duration;
    public float Duration
    {
        get => _duration;
        set => _duration = value;
    }

    [SerializeField]
    private float _effectDuration;
    public float EffectDuration
    {
        get => _effectDuration;
        set => _effectDuration = value;
    }

    public DashStats(
        float damage,
        float cooldown,
        float force,
        float duration,
        float effectDuration
    )
        : base(damage, cooldown)
    {
        _force = force;
        _duration = duration;
        _effectDuration = effectDuration;
    }
}
