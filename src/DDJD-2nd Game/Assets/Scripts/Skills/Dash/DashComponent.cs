using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DashComponent : SkillComponent
{
    protected DashSkill _dashSkill;
    protected Vector3 _dashDirection;

    protected FMOD.Studio.PARAMETER_ID _elementParameterId;

    public override void SetSkill(Skill skill)
    {
        base.SetSkill(skill);
        _dashSkill = (DashSkill)skill;

        if (_soundEmitter != null)
        {
            _soundEmitter.SetParameterWithLabel(
                "dash",
                _elementParameterId,
                _dashSkill.Element.SfxDamageLabel,
                false
            );
        }
    }

    // destroy the gameobject after the dash is over
    protected virtual void Start()
    {
        if (_soundEmitter != null)
        {
            _elementParameterId = _soundEmitter.GetParameterId("dash", "Dash Type");
        }
    }

    public virtual void SetDashDirection(Vector3 direction)
    {
        _dashDirection = direction;
    }

    public virtual void OnDashEnd() { }
}
