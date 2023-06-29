public class MissionMenuState : GenericState
{
    protected Player _context;

    public MissionMenuState(Player context)
        : base(context, null)
    {
        _context = context;
    }

    public override void Enter()
    {
        _context.Input.OnMissionKeydown += OnMissionKeydown;
        _context.Input.OnExitKeydown += OnMissionKeydown;
        _context.UIController.OpenMissions(true);
    }

    public override void Exit()
    {
        base.Exit();
        _context.Input.OnMissionKeydown -= OnMissionKeydown;
        _context.Input.OnExitKeydown += OnMissionKeydown;
        _context.UIController.OpenMissions(false);
    }

    public override void StateUpdate() { }

    private void OnMissionKeydown()
    {
        _context.ChangeState(_context.Factory.Playable());
    }
}
