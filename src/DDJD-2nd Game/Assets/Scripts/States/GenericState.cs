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
        set { _superstate = value; }
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

    public virtual void Enter()
    {
        if (_substate != null)
        {
            _substate.Enter();
        }
    }

    public virtual void Exit()
    {
        if (_substate != null)
        {
            _substate.Exit();
        }
    }

    public virtual bool CanChangeState(GenericState state)
    {
        if (_substate != null)
        {
            return _substate.CanHaveSuperState(state);
        }
        return true;
    }

    public virtual bool CanHaveSuperState(GenericState state)
    {
        if (_substate != null)
        {
            return _substate.CanHaveSuperState(state);
        }
        return true;
    }

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
            state.Superstate = this;
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

    public GenericState GetSubState()
    {
        return _substate;
    }
}
