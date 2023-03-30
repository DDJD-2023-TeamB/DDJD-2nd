using UnityEngine;
[System.Serializable]
public class ProjectileStats : SpellStats
{
    [SerializeField]
    private float _speed;
    public float Speed { get => _speed; set => _speed = value; }
    public ProjectileStats(float damage, float cooldown, float speed) : base(damage, cooldown)
    {
        _speed = speed;
    }
}