public class PlayerStateFactory : StateFactory
{
    Player _context;

    public PlayerStateFactory(Player context)
    {
        _context = context;
    }

    public IdleState Idle(GenericState superState)
    {
        return new IdleState(_context, superState);
    }

    public ChangeActiveElementState ChangeElement(GenericState superState)
    {
        return new ChangeActiveElementState(_context, superState);
    }

    public MoveState Move(GenericState superState)
    {
        return new MoveState(_context, superState);
    }

    public RunState Run(GenericState superState)
    {
        return new RunState(_context, superState);
    }

    public PlayableState Playable()
    {
        return new PlayableState(_context);
    }

    public AimingState Aiming(GenericState superState)
    {
        return new AimingState(_context, superState);
    }

    public NotAimingState NotAiming(GenericState superState)
    {
        return new NotAimingState(_context, superState);
    }

    public AirborneState Airborne(GenericState superState)
    {
        return new AirborneState(_context, superState);
    }

    public GroundedState Grounded(GenericState superState)
    {
        return new GroundedState(_context, superState);
    }

    public MeleeAttackingState GetMeleeAttackingState(GenericState superState)
    {
        return new MeleeAttackingState(_context, superState);
    }

    public DashState Dash(GenericState superState, DashStats stats, DashSkill skill = null)
    {
        // TODO I think we can get the skill from the context
        return new DashState(_context, superState, stats, skill);
    }

    public ChangeSpellState ChangeSpell(GenericState superState)
    {
        return new ChangeSpellState(_context, superState);
    }

    public InventoryState Inventory()
    {
        return new InventoryState(_context);
    }

    public MissionMenuState MissionMenu()
    {
        return new MissionMenuState(_context);
    }

    public MenuState Menu()
    {
        return new MenuState(_context);
    }

    public InteractingState Interacting(GenericState superState)
    {
        return new InteractingState(_context, superState);
    }

    public AbsorbingState Absorbing(GenericState superState)
    {
        return new AbsorbingState(_context, superState);
    }

    public DeadState Dead()
    {
        return new DeadState(_context, null);
    }
}
