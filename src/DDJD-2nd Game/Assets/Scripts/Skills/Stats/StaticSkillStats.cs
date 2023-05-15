using UnityEngine;
using MyBox;

[System.Serializable]
public class StaticSkillStats : SkillStats
{
    [SerializeField]
    private float _duration;
    public float Duration
    {
        get => _duration;
        set => _duration = value;
    }

    [SerializeField]
    private float _maxDistance = 30f;
    public float MaxDistance
    {
        get => _maxDistance;
        set => _maxDistance = value;
    }

    public StaticSkillStats(float damage, float cooldown, float duration, float maxDistance)
        : base(damage, cooldown)
    {
        _duration = duration;
        _maxDistance = maxDistance;
    }
}
