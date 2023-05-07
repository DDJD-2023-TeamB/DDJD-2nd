public class EnemyStates
{
    protected EnemyChaseState _chaseState;
    protected EnemyAttackState _attackState;

    protected EnemyIdleState _idleState;

    public EnemyStates(
        EnemyChaseState chaseState,
        EnemyAttackState attackState,
        EnemyIdleState idleState
    )
    {
        _attackState = attackState;
        _chaseState = chaseState;
        _idleState = idleState;
    }

    public EnemyChaseState ChaseState
    {
        get { return _chaseState; }
    }

    public EnemyAttackState AttackState
    {
        get { return _attackState; }
    }

    public EnemyIdleState IdleState
    {
        get { return _idleState; }
    }
}
