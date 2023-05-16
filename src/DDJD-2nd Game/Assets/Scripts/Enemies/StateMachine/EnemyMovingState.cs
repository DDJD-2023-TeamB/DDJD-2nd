using UnityEngine;

public class EnemyMovingState : GenericState
{
    protected BasicEnemy _context;

    public EnemyMovingState(BasicEnemy enemy)
        : base(enemy)
    {
        _context = enemy;
    }

    public override void Enter()
    {
        _context.Rigidbody.isKinematic = true;
        _context.NavMeshAgent.enabled = true;
    }

    public override void StateUpdate()
    {
        float forwardSpeed =
            Vector3.Dot(_context.NavMeshAgent.velocity, _context.transform.forward)
            / _context.NavMeshAgent.speed;
        float rightSpeed =
            Vector3.Dot(_context.NavMeshAgent.velocity, _context.transform.right)
            / _context.NavMeshAgent.speed;
        _context.Animator.SetFloat(_context.ForwardSpeedHash, forwardSpeed);
        _context.Animator.SetFloat(_context.RightSpeedHash, rightSpeed);
    }

    public override void Exit()
    {
        base.Exit();
        _context.NavMeshAgent.ResetPath();
        _context.NavMeshAgent.enabled = false;
        _context.Rigidbody.isKinematic = false;
        _context.Animator.SetFloat(_context.ForwardSpeedHash, 0.0f);
        _context.Animator.SetFloat(_context.RightSpeedHash, 0.0f);
    }
}
