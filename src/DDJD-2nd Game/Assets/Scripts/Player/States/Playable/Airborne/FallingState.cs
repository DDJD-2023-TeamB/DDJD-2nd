using UnityEngine;
using System.Collections;

public class FallingState : GenericState
{
    private Player _context;

    public FallingState(StateContext context, GenericState superState)
        : base(context, superState)
    {
        _context = (Player)context;
    }

    public override void Enter() { }

    public override void Exit()
    {
        base.Exit();
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

    // Check state and substate, return true if state is changed
    private bool CheckCurrentStates()
    {
        return false;
    }
}
