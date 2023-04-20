using UnityEngine;
using MyBox;

public abstract class SkillStats
{
    [SerializeField]
    protected float _damage;

    [SerializeField]
    protected float _force;

    [SerializeField, Range(0, 1)]
    protected float _damageToForceMultiplier;

    [SerializeField]
    protected float _cooldown;

    [SerializeField]
    protected float _castTime;
    public float CastTime
    {
        get => _castTime;
        set => _castTime = value;
    }

    [SerializeField]
    private CastType _castType;
    public CastType CastType
    {
        get => _castType;
        set => _castType = value;
    }

    [ConditionalField(nameof(_castType), false, CastType.Charge)]
    [SerializeField]
    protected float _maxChargeTime;
    public float MaxChargeTime
    {
        get => _maxChargeTime;
        set => _maxChargeTime = value;
    }

    public float Damage
    {
        get => _damage;
        set => _damage = value;
    }
    public float Cooldown
    {
        get => _cooldown;
        set => _cooldown = value;
    }

    public float Force
    {
        get => _force;
        set => _force = value;
    }

    public float DamageToForceMultiplier
    {
        get => _damageToForceMultiplier;
        set => _damageToForceMultiplier = value;
    }

    public float ForceWithDamage()
    {
        return _force + _damage * _damageToForceMultiplier;
    }

    public SkillStats(float damage, float cooldown)
    {
        _damage = damage;
        _cooldown = cooldown;
    }
}
