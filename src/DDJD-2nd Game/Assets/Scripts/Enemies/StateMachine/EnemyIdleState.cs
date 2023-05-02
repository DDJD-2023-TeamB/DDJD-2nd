public class EnemyIdleState : GenericState
{
    private BasicEnemy _context;

    public EnemyIdleState(BasicEnemy enemy)
        : base(enemy)
    {
        _context = enemy;
    }

    public override void Enter()
    {
        _context.Animator.SetBool("Idle", true);
    }

    public override void StateUpdate()
    {
        if (false)
        {
            _context.ChangeState(new EnemyChaseState(_context));
        }
    }

    public override void Exit()
    {
        base.Exit();
        _context.Animator.SetBool("Idle", false);
    }

    public override bool CanChangeState(GenericState state)
    {
        return true;
    }
}
