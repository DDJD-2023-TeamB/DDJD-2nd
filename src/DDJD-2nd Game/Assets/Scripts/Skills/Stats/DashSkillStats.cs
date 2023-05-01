using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DashSkillStats : SkillStats
{
    [SerializeField]
    private float _effectDuration;
    public float EffectDuration
    {
        get => _effectDuration;
        set => _effectDuration = value;
    }

    [SerializeField]
    private float _damageRate;
    public float DamageRate
    {
        get => _damageRate;
        set => _damageRate = value;
    }

    public DashSkillStats(float damage, float cooldown, float effectDuration, float damageRate)
        : base(damage, cooldown)
    {
        _effectDuration = effectDuration;
        _damageRate = damageRate;
    }
}
