public class ChangeSpellState : MovableState
{
    public ChangeSpellState(StateContext context, GenericState superState)
        : base(context, superState) { }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        _context.UIController.OpenLeftSpell(false);
        _context.UIController.OpenRightSpell(false);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        if (!_context.Input.IsLeftShooting && !_context.Input.IsRightShooting)
        {
            _context.ChangeState(_context.Factory.Playable());
            return;
        }

        _context.UIController.OpenLeftSpell(_context.Input.IsLeftShooting);
        _context.UIController.OpenRightSpell(_context.Input.IsRightShooting);
    }
}
