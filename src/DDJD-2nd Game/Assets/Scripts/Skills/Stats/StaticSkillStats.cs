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

    [
        SerializeField,
        Tooltip("If true, the skill will tick every TickRate seconds for the same object."),
    ]
    private bool _isContinuous;
    public bool IsContinuous
    {
        get => _isContinuous;
        set => _isContinuous = value;
    }

    [SerializeField]
    [ConditionalField(nameof(_isContinuous), false, true)]
    private float _tickRate = 0.5f;
    public float TickRate
    {
        get => _tickRate;
        set => _tickRate = value;
    }

    public StaticSkillStats(float damage, float cooldown, float duration)
        : base(damage, cooldown)
    {
        _duration = duration;
    }
}
