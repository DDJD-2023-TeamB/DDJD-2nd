public class EnemyAttackState : GenericState
{
    private BasicEnemy _context;

    public EnemyAttackState(BasicEnemy enemy)
        : base(enemy)
    {
        _context = enemy;
    }

    public override void Enter()
    {
        _context.Animator.SetBool("Attack", true);
    }

    public override void StateUpdate()
    {
        if (false)
        {
            _context.ChangeState(new EnemyIdleState(_context));
        }
    }

    public override void Exit()
    {
        base.Exit();
        _context.Animator.SetBool("Attack", false);
    }
}
