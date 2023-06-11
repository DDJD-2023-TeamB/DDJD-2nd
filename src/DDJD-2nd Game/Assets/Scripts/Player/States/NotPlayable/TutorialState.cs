public class TutorialState : GenericState
{
    protected Player _context;

    public TutorialState(Player context)
        : base(context, null)
    {
        _context = context;
    }

    public override void Enter()
    {
        // Open tutorial with a key if wanted
        _context.Input.OnTutorialKeydown += OnMenuKeydown;
        _context.UIController.OpenTutorial(true);
    }

    public override void Exit()
    {
        base.Exit();
        _context.Input.OnTutorialKeydown -= OnMenuKeydown;
        _context.UIController.OpenTutorial(false);
    }

    public override void StateUpdate() { }

    private void OnMenuKeydown()
    {
        _context.ChangeState(_context.Factory.Playable());
    }
}
