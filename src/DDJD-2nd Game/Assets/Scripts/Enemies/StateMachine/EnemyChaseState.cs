using UnityEngine;

public class EnemyChaseState : GenericState
{
    private BasicEnemy _context;

    public EnemyChaseState(BasicEnemy enemy)
        : base(enemy)
    {
        _context = enemy;
    }

    public override void Enter()
    {
        _context.Animator.SetBool("Chase", true);
    }

    public override void StateUpdate()
    {
        if (IsInAttackRange())
        {
            _context.ChangeState(new EnemyAttackState(_context));
        }
        else if (false)
        {
            _context.ChangeState(new EnemyIdleState(_context));
        }
    }

    public override void Exit()
    {
        base.Exit();
        _context.Animator.SetBool("Chase", false);
    }

    private bool IsInAttackRange()
    {
        // return Vector3.Distance(_context.transform.position, _context.transform.position) <= _context.AttackRange;
        return false;
    }
}
