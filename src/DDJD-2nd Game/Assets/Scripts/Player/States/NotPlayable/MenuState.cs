public class MenuState : GenericState
{
    protected Player _context;

    public MenuState(Player context)
        : base(context, null)
    {
        _context = context;
    }

    public override void Enter()
    {
        _context.Input.OnMenuKeydown += OnMenuKeydown;
        _context.UIController.OpenMenu(true);
    }

    public override void Exit()
    {
        base.Exit();
        _context.Input.OnMenuKeydown -= OnMenuKeydown;
        _context.UIController.OpenMenu(false);
    }

    public override void StateUpdate() { }

    private void OnMenuKeydown()
    {
        _context.ChangeState(_context.Factory.Playable());
    }
}
