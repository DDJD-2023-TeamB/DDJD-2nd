using UnityEngine;

public class EnemyIdleState : GenericState
{
    protected BasicEnemy _context;

    public EnemyIdleState(BasicEnemy enemy)
        : base(enemy)
    {
        _context = enemy;
    }

    public override void Enter() { }

    public override void StateUpdate()
    {
        if (IsInAggroRange())
        {
            _context.ChangeState(_context.States.ChaseState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override bool CanChangeState(GenericState state)
    {
        return true;
    }

    protected bool IsInAggroRange()
    {
        return Vector3.Distance(_context.transform.position, _context.Player.transform.position)
            <= _context.AggroRange;
    }
}
