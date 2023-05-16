using UnityEngine;

public class EnemyMovingState : EnemyState
{
    public EnemyMovingState(BasicEnemy enemy)
        : base(enemy) { }

    public override void Enter()
    {
        base.Enter();
        _context.Rigidbody.isKinematic = true;
        _context.NavMeshAgent.enabled = true;
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
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
        _context.Animator.SetFloat(_context.ForwardSpeedHash, 0.0f);
        _context.Animator.SetFloat(_context.RightSpeedHash, 0.0f);
    }
}
