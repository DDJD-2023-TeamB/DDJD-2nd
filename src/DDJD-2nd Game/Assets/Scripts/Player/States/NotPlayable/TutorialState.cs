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
        _context.UIController.OpenTutorial(true);
    }

    public override void Exit()
    {
        base.Exit();
        _context.UIController.OpenTutorial(false);
    }

    public override void StateUpdate() { }
}
