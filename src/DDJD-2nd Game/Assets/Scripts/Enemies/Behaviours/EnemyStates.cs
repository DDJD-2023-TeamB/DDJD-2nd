public class EnemyStates
{
    protected EnemyChaseState _chaseState;
    protected EnemyAttackState _attackState;

    public EnemyStates(EnemyChaseState chaseState, EnemyAttackState attackState)
    {
        _attackState = attackState;
        _chaseState = chaseState;
    }

    public EnemyChaseState ChaseState
    {
        get { return _chaseState; }
    }

    public EnemyAttackState AttackState
    {
        get { return _attackState; }
    }
}
