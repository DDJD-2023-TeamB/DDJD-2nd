using UnityEngine;
using System.Collections;

public class EnemySimpleMovementState : EnemyChaseState
{
    private Coroutine _dashCoroutine;

    public EnemySimpleMovementState(BasicEnemy enemy)
        : base(enemy) { }

    protected override void Move()
    {
        _context.NavMeshAgent.SetDestination(_context.Player.transform.position);
    }

    public override void Enter()
    {
        base.Enter();
        _context.NavMeshAgent.enabled = true;
        _context.Rigidbody.isKinematic = true;
        if (_context.EnemySkills.UsesDash)
        {
            _dashCoroutine = _context.StartCoroutine(
                EnemyMovementStateUtils.DashCoroutine(_context, 10.0f, this)
            );
        }
    }

    public override void Exit()
    {
        base.Exit();
        if (_dashCoroutine != null)
        {
            _context.StopCoroutine(_dashCoroutine);
        }
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
}
