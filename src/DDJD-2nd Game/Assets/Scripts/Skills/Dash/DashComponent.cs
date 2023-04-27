using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DashComponent : SkillComponent
{
    protected DashSkill _skill;

    public override void SetSkill(Skill skill)
    {
        base.SetSkill(skill);
        _skill = (DashSkill)skill;
    }

    // destroy the gameobject after the dash is over
    protected virtual void Start()
    {
        Destroy(gameObject, _skill.DashSkillStats.EffectDuration);
    }
}
