using UnityEngine;
using MyBox;

public abstract class SkillStats
{
    [SerializeField]
    protected float _damage;

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
    private ShootType _shootType;
    public ShootType ShootType
    {
        get => _shootType;
        set => _shootType = value;
    }

    [ConditionalField(nameof(_shootType), false, ShootType.Charge)]
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

    public SkillStats(float damage, float cooldown)
    {
        _damage = damage;
        _cooldown = cooldown;
    }
}
