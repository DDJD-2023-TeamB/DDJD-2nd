using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dash", menuName = "Scriptable Objects/Skills/Dash", order = 2)]
public class Dash : Skill
{
    [SerializeField]
    private DashStats _stats;

    // Unity doesn't support covariant return types
    public override SkillStats Stats
    {
        get { return (SkillStats)_stats; }
    }
    public DashStats DashStats
    {
        get { return _stats; }
    }
}
