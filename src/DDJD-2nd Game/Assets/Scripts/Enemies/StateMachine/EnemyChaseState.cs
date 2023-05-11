using UnityEngine;

public class EnemyChaseState : EnemyMovingState
{
    public EnemyChaseState(BasicEnemy enemy)
        : base(enemy) { }

    public override void Enter()
    {
        base.Enter();
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        if (IsInAttackRange())
        {
            _context.ChangeState(_context.States.AttackState);
            return;
        }
        else if (!IsInAggroRange())
        {
            _context.ChangeState(new EnemyIdleState(_context));
            return;
        }
        Move();
    }

    public override void Exit()
    {
        base.Exit();
    }

    protected virtual void Move() { }

    protected bool IsInAttackRange()
    {
        return Vector3.Distance(_context.transform.position, _context.Player.transform.position)
            <= _context.AttackRange;
    }

    protected bool IsInAggroRange()
    {
        return Vector3.Distance(_context.transform.position, _context.Player.transform.position)
            <= _context.AggroRange;
    }
}
