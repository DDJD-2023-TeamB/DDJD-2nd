using UnityEngine;

public class EnemySimpleMovementState : EnemyChaseState
{
    public EnemySimpleMovementState(BasicEnemy enemy)
        : base(enemy) { }

    protected override void Move()
    {
        _context.NavMeshAgent.SetDestination(_context.Player.transform.position);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Enter()
    {
        base.Enter();
    }
}
