using FMOD;
using System.Diagnostics;
using UnityEngine;

public class ChangeActiveElementState : MovableState
{
    public ChangeActiveElementState(StateContext context, GenericState superState)
        : base(context, superState) { }

    public override void Enter()
    {
        base.Enter();
        _context.UIController.OpenActiveElement(true);
    }

    public override void Exit()
    {
        base.Exit();
        _context.UIController.OpenActiveElement(false);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        if (!_context.Input.IsChangingActiveElement)
        {
            _context.ChangeState(_context.Factory.Playable());
            return;
        }

        _context.UIController.OpenActiveElement(_context.Input.IsChangingActiveElement);
    }
}
