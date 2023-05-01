using UnityEngine;
using System.Collections;

public class AirborneState : GenericState
{
    private Player _context;

    public AirborneState(StateContext context, GenericState superState)
        : base(context, superState)
    {
        _context = (Player)context;
    }

    public override void Enter()
    {
        _context.Animator.ResetTrigger("Jump");
        _context.Animator.SetBool("IsGrounded", false);
    }

    public override void Exit()
    {
        base.Exit();
        _context.Animator.SetBool("IsGrounded", true);
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
        if (CheckCurrentStates())
        {
            return;
        }
    }

    private bool CheckCurrentStates()
    {
        if (MovementUtils.IsGrounded(_context.Rigidbody))
        {
            _superstate.ChangeSubState(_context.Factory.Grounded(_superstate));
            return true;
        }

        return false;
    }
}
