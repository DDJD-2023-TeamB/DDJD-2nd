using UnityEngine;

public abstract class MovableState : GenericState{

    protected Player _context;

    public override void Enter()
    {
        CheckAirbone();
    }

    public MovableState(StateContext context, GenericState superState)
        : base(context, superState)
    {
        _context = (Player)context;
    }

    public override void StateUpdate(){
        CheckAirbone();
    }

    private void CheckAirbone()
    {
        if (!MovementUtils.IsGrounded(_context.Rigidbody) && !(_substate is AirborneState))
        {
            ChangeSubState(_context.Factory.Airborne(_substate));
        }
        else if (MovementUtils.IsGrounded(_context.Rigidbody) && !(_substate is GroundedState))
        {
            ChangeSubState(_context.Factory.Grounded(_substate));
        }
    }
}