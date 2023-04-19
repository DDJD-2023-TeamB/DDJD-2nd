using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericState
{
    protected GenericState _superstate;
    protected GenericState _substate;
    public GenericState Superstate
    {
        get { return _superstate; }
    }
    public GenericState Substate
    {
        get { return _substate; }
    }
    private StateContext _context;

    public GenericState(StateContext context, GenericState superState = null)
    {
        _context = context;
        _superstate = superState;
    }

    public abstract void Enter();
    public abstract void Exit();
    public abstract bool CanChangeState(GenericState state);
    public abstract void StateUpdate();

    public bool ChangeSubState(GenericState state)
    {
        if (!CanChangeState(state))
        {
            return false;
        }
        if (_substate != null)
        {
            if (!_substate.CanChangeState(state))
            {
                return false;
            }
            _substate.ChangeSubState(null);
            _substate.Exit();
        }
        _substate = state;
        if (state != null)
        {
            _substate.Enter();
        }

        return true;
    }

    public void Update()
    {
        StateUpdate();
        if (_substate != null)
        {
            _substate.Update();
        }
    }
}
