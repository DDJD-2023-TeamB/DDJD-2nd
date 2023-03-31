using UnityEngine;
public abstract class SkillStats {
    [SerializeField]
    protected float _damage;
    [SerializeField]
    protected float _cooldown;

    public float Damage { get => _damage; set => _damage = value; }
    public float Cooldown { get => _cooldown; set => _cooldown = value; }
    public SkillStats(float damage, float cooldown)
    {
        _damage = damage;
        _cooldown = cooldown;
    }
}