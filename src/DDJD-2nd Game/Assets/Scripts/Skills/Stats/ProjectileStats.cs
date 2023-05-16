using UnityEngine;

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

    public ProjectileStats(float damage, float cooldown, float speed)
        : base(damage, cooldown)
    {
        _speed = speed;
    }
}
