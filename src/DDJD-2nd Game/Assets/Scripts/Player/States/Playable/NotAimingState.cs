using UnityEngine;
using System.Collections;

public class NotAimingState : MovableState
{

    public NotAimingState(StateContext context, GenericState superState)
        : base(context, superState)
    {
        
    }

    private float _attackCD;
    private float _timeUntilNextAttack;

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit() { }

    public override bool CanChangeState(GenericState state)
    {
        return true;
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        CheckDash();
    }

    private void CheckDash()
    {
        if (!_context.Input.IsDashing)
            return;

        if (_context.PlayerSkills.DashSkill != null)
        {
            UseDashSkill(_context.PlayerSkills.DashSkill);
        }
        else
        {
            // TODO: Normal dash
        }
    }

    private void UseDashSkill(Dash dashSkill)
    {
        if (_context.PlayerSkills.IsSkillOnCooldown(dashSkill))
        {
            return;
        }
        _context.PlayerSkills.StartSkillCooldown(dashSkill);
        _context.DashComponent.DashWithSkill(dashSkill);
        // TODO: Trigger animation
    }
}
