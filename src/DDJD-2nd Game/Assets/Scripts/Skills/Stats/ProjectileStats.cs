using UnityEngine;
using MyBox;

[System.Serializable]
public class ProjectileStats : SkillStats
{
    [Header("Projectile Stats")]
    [SerializeField]
    private float _speed;

    [SerializeField]
    private bool _destroyOnImpact;

    public bool DestroyOnImpact
    {
        get => _destroyOnImpact;
        set => _destroyOnImpact = value;
    }

    [SerializeField]
    private float _range;
    public float Range
    {
        get => _range;
        set => _range = value;
    }

    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }

    [SerializeField]
    private bool _isDestructible;

    [SerializeField]
    [ConditionalField(nameof(_isDestructible), false, true)]
    private float _maxHealth = 50.0f;

    [SerializeField]
    private float _damageToSpellsMultiplier = 1.0f;

    public ProjectileStats(float damage, float cooldown, float speed)
        : base(damage, cooldown)
    {
        _speed = speed;
    }

    public bool IsDestructible
    {
        get => _isDestructible;
        set => _isDestructible = value;
    }

    public float MaxHealth
    {
        get => _maxHealth;
        set => _maxHealth = value;
    }

    public float DamageToSpellsMultiplier
    {
        get => _damageToSpellsMultiplier;
        set => _damageToSpellsMultiplier = value;
    }
}
