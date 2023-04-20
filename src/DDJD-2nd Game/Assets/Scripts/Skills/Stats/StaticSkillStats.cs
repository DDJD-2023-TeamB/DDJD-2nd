using UnityEngine;

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

    public StaticSkillStats(float damage, float cooldown, float duration)
        : base(damage, cooldown)
    {
        _duration = duration;
    }
}
