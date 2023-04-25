using UnityEngine;
using System.Collections;

public class NotAimingState : MovableState
{
    public NotAimingState(StateContext context, GenericState superState)
        : base(context, superState) { }

    private float _attackCD;
    private float _timeUntilNextAttack;

    public override void Enter()
    {
        base.Enter();
        _context.Input.OnMeleeAttackKeydown += OnMeleeAttackKeydown;
        _context.Input.OnMeleeAttackKeyup += OnMeleeAttackKeyup;
    }

    public override void Exit()
    {
        base.Exit();
        _context.Input.OnMeleeAttackKeydown -= OnMeleeAttackKeydown;
        _context.Input.OnMeleeAttackKeyup -= OnMeleeAttackKeyup;
    }

    public override bool CanChangeState(GenericState state)
    {
        if (!base.CanChangeState(state))
        {
            return false;
        }
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
        if (
            !_context.Input.IsDashing
            || _substate is DashState
            || _context.Dashable.IsDashOnCooldown()
        )
        {
            return;
        }

        ChangeSubState(
            _context.Factory.Dash(
                this,
                _context.PlayerSkills.DashStats,
                _context.PlayerSkills.CurrentElement?.DashSkill
            )
        );
    }
}
