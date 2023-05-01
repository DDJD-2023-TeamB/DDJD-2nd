using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dash", menuName = "Scriptable Objects/Skills/Dash", order = 2)]
public class DashSkill : Skill
{
    [SerializeField]
    private DashSkillStats _skillStats;

    [SerializeField]
    private DashStats _dashStats;

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
}