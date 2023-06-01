public class InventoryState : GenericState
{
    protected Player _context;

    public InventoryState(Player context)
        : base(context, null)
    {
        _context = context;
    }

    public override void Enter()
    {
        _context.Input.OnInventoryKeydown += OnInventoryKeydown;
        _context.UIController.OpenInventory(true);
    }

    public override void Exit()
    {
        base.Exit();
        _context.Input.OnInventoryKeydown -= OnInventoryKeydown;
        _context.UIController.OpenInventory(false);
    }

    public override void StateUpdate() { }

    private void OnInventoryKeydown()
    {
        _context.ChangeState(_context.Factory.Playable());
    }
}
