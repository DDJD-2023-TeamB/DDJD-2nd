using UnityEngine;

public class EnemyChaseState : GenericState
{
    protected BasicEnemy _context;

    public EnemyChaseState(BasicEnemy enemy)
        : base(enemy)
    {
        _context = enemy;
    }

    public override void Enter()
    {
        _context.NavMeshAgent.enabled = true;
    }

    public override void StateUpdate()
    {
        if (IsInAttackRange())
        {
            _context.ChangeState(_context.States.AttackState);
            return;
        }
        else if (!IsInAggroRange())
        {
            Debug.Log("Going to idle");
            _context.ChangeState(new EnemyIdleState(_context));
            return;
        }

        _context.Animator.SetFloat(
            _context.ForwardSpeedHash,
            _context.NavMeshAgent.velocity.magnitude
        );
        Move();
    }

    public override void Exit()
    {
        base.Exit();
        _context.Animator.SetFloat(_context.ForwardSpeedHash, 0.0f);
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
