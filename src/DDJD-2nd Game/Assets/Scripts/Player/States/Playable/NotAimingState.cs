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
        Debug.Log("Entering not aiming state");
        _context.Input.OnMeleeAttackKeydown += OnMeleeAttackKeydown;
        _context.Input.OnMeleeAttackKeyup += OnMeleeAttackKeyup;
    }

    public override void Exit() {
        _context.Input.OnMeleeAttackKeydown -= OnMeleeAttackKeydown;
        _context.Input.OnMeleeAttackKeyup -= OnMeleeAttackKeyup;
     }

    public override bool CanChangeState(GenericState state)
    {
        return true;
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        CheckDash();
    }

    private void OnMeleeAttackKeydown()
    {
        _context.Animator.SetBool("IsAttacking", true);
    }

    private void OnMeleeAttackKeyup()
    {
        _context.Animator.SetBool("IsAttacking", false);
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
