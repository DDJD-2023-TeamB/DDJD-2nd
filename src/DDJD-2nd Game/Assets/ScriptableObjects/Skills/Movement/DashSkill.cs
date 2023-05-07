using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dash", menuName = "Scriptable Objects/Skills/Dash", order = 4)]
public class DashSkill : Skill
{
    [SerializeField]
    private DashSkillStats _skillStats;

    [SerializeField]
    private DashStats _dashStats;

    [SerializeField]
    private bool _canDashInAir = true;

    // Unity doesn't support covariant return types
    public override SkillStats SkillStats
    {
        get { return (SkillStats)_skillStats; }
    }
    public DashSkillStats DashSkillStats
    {
        get { return _skillStats; }
    }
    public DashStats DashStats
    {
        get { return _dashStats; }
    }

    public bool CanDashInAir
    {
        get { return _canDashInAir; }
    }
}
