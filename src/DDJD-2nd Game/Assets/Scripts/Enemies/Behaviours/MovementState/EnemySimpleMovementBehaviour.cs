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
            _dashCoroutine = _context.StartCoroutine(DashCoroutine());
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
    }

    private IEnumerator DashCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            bool canDash =
                _context.NavMeshAgent.remainingDistance > 10.0f
                && _context.EnemyDashable.IsDashReady();
            if (canDash)
            {
                DashToPosition(_context.NavMeshAgent.destination);
            }
        }
    }

    private void DashToPosition(Vector3 position)
    {
        Vector3 direction = (position - _context.transform.position).normalized;
        if (direction.y < 0)
        {
            direction.y = 0;
        }
        _context.ChangeState(new EnemyDashState(_context, direction, this));
    }
}
