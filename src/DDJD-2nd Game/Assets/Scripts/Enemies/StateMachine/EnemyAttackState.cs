using UnityEngine;

public class EnemyAttackState : GenericState
{
    protected BasicEnemy _context;

    public EnemyAttackState(BasicEnemy enemy)
        : base(enemy)
    {
        _context = enemy;
    }

    public override void Enter()
    {
        _context.NavMeshAgent.enabled = true;
        _context.Animator.SetBool("IsAiming", true);
    }

    public override void StateUpdate()
    {
        if (!IsInAttackRange())
        {
            _context.ChangeState(_context.States.ChaseState);
            return;
        }

        Attack();
        Move();
    }

    protected virtual void Attack() { }

    protected virtual void Move() { }

    public override void Exit()
    {
        base.Exit();
        _context.Animator.SetBool("IsAiming", true);
    }

    protected bool IsInAttackRange()
    {
        return Vector3.Distance(_context.transform.position, _context.Player.transform.position)
            <= _context.AttackRange;
    }
}
