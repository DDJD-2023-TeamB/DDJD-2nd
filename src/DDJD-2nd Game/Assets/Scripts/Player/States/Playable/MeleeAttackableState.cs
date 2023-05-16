using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeAttackableState : GenericState
{
    private Player _context;

    public MeleeAttackableState(StateContext context, GenericState superState)
        : base(context, superState)
    {
        _context = (Player)context;
    }

    public override void StateUpdate()
    {
        if (_substate != null && !_substate.CanChangeState(null))
        {
            return;
        }
        if (_context.Input.IsMeleeAttacking && !(_substate is MeleeAttackingState))
        {
            ChangeSubState(_context.Factory.GetMeleeAttackingState(this));
        }
    }
}
