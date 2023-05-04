using UnityEngine;
using System.Collections;

public class EnemyKnockdownState : GenericState
{
    protected BasicEnemy _context;

    private float _force;
    private Vector3 _hitPoint;
    private Vector3 _hitDirection;
    private float _timeToRecover = 2.0f;

    private float _timeGrounded = 0.0f;

    public EnemyKnockdownState(
        BasicEnemy enemy,
        float force,
        Vector3 hitPoint,
        Vector3 hitDirection
    )
        : base(enemy)
    {
        _context = enemy;
        _force = force;
        _hitPoint = hitPoint;
        _hitDirection = hitDirection;
    }

    public override void Enter()
    {
        _context.NavMeshAgent.enabled = false;
        _context.RagdollController.ActivateRagdoll();
        _context.RagdollController.PushRagdoll(
            force: (int)_force,
            hitPoint: _hitPoint,
            hitDirection: _hitDirection
        );
        //_context.StartCoroutine(RecoverFromKnockdown());
        _timeGrounded = 0.0f;
    }

    public override void StateUpdate()
    {
        if (_context.RagdollController.GetVelocity() <= 0.25f)
        {
            _timeGrounded += Time.deltaTime;
            if (_timeGrounded >= _timeToRecover)
            {
                RecoverFromKnockdown();
            }
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

    private void RecoverFromKnockdown()
    {
        _context.ChangeState(new EnemyRecoveringResetBonesState(_context));
    }
}
