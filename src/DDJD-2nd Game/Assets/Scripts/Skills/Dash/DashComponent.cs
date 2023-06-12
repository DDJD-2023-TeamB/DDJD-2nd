using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DashComponent : SkillComponent
{
    protected DashSkill _dashSkill;
    protected Vector3 _dashDirection;

    public override void SetSkill(Skill skill)
    {
        base.SetSkill(skill);
        _dashSkill = (DashSkill)skill;
    }

    // destroy the gameobject after the dash is over
    protected virtual void Start() { }

    public virtual void SetDashDirection(Vector3 direction)
    {
        _dashDirection = direction;
    }

    public virtual void OnDashEnd() { }
}
