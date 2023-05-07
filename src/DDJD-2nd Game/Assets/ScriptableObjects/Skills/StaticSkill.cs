using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Static", menuName = "Scriptable Objects/Skills/Static", order = 3)]
public class StaticSkill : AimedSkill
{
    [SerializeField]
    private GameObject _impactPrefab;
    public GameObject ImpactPrefab => _impactPrefab;

    [SerializeField]
    private StaticSkillStats _stats;

    // Unity doesn't support covariant return types
    public override SkillStats SkillStats
    {
        get { return (SkillStats)_stats; }
        set { _stats = (StaticSkillStats)value; }
    }
    public StaticSkillStats StaticStats
    {
        get { return _stats; }
    }
}
