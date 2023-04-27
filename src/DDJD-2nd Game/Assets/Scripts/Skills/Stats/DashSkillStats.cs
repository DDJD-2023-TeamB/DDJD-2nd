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

    public DashSkillStats(float damage, float cooldown, float effectDuration)
        : base(damage, cooldown)
    {
        _effectDuration = effectDuration;
    }
}
