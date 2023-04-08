using UnityEngine;
using System.Collections;

public class NotAimingState : GenericState
{
    private Player _context;

    public NotAimingState(StateContext context, GenericState superState)
        : base(context, superState)
    {
        _context = (Player)context;
    }

    private float _attackCD;
    private float _timeUntilNextAttack;

    public override void Enter()
    {
        _context.AimCamera.Priority = 5;
        _context.Animator.SetBool("IsAiming", false);
        _context.AimComponent.StopAim();
    }

    public override void Exit() { }

    public override bool CanChangeState(GenericState state)
    {
        return true;
    }

    public override void StateUpdate()
    {
        CheckDash();
    }

    public static bool GiveSubState(GenericState state, StateContext context)
    {
        if (!(context is Player))
        {
            return false;
        }
        Player player = (Player)context;
        if (!(state.Substate is NotAimingState) && !player.Input.IsAiming)
        {
            state.ChangeSubState(player.Factory.NotAiming(state));
            return true;
        }
        return false;
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
