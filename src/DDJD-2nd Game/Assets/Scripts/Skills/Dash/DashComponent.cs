using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DashComponent : SkillComponent
{
    protected DashStats _stats;
    protected Dash _skill;

    public override void SetSkill(Skill skill)
    {
        _skill = (Dash)skill;
        _stats = _skill.DashStats;
    }

    // destroy the gameobject after the dash is over
    protected virtual void Start()
    {
        Destroy(gameObject, _stats.EffectDuration);
    }
}
