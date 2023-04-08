using UnityEngine;
using System.Collections;

public class IdleState : GenericState
{
    private Player _context;

    public IdleState(StateContext context, GenericState superState)
        : base(context, superState)
    {
        _context = (Player)context;
    }

    public override void Enter()
    {
        _context.Animator.SetFloat("ForwardSpeed", 0f);
        _context.Animator.SetFloat("RightSpeed", 0f);
    }

    public override void Exit() { }

    public override bool CanChangeState(GenericState state)
    {
        return true;
    }

    public override void StateUpdate()
    {
        if (CheckCurrentStates())
        {
            return;
        }
    }

    // Check state and substate, return true if state is changed
    private bool CheckCurrentStates()
    {
        if (!AimingState.GiveSubState(this, _context))
        {
            NotAimingState.GiveSubState(this, _context);
        }

        if (_context.Input.MoveInput != Vector2.zero)
        {
            _superstate.ChangeSubState(_context.Factory.Move(_superstate));
            return true;
        }
        return false;
    }
}
