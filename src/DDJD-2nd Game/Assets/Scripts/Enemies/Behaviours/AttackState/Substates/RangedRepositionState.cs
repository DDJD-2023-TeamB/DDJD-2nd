using UnityEngine;
using System.Collections;

public class RangedRepositionState : EnemyMovingState
{
    protected new RangedEnemy _context;

    private int _tries = 0;
    private int _maxTries = 5;

    private Vector3 _newPosition;

    private Coroutine _dashCoroutine;

    public RangedRepositionState(RangedEnemy enemy)
        : base(enemy)
    {
        _context = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        StartReposition();
        _context.Animator.SetBool("IsAiming", false);
        _context.AimComponent.StopAim();
        if (_context.EnemySkills.UsesDash)
        {
            _dashCoroutine = _context.StartCoroutine(DashCoroutine());
        }
    }

    public override void Exit()
    {
        base.Exit();
        _context.Animator.SetBool("IsAiming", true);
        _context.AimComponent.StartAim();
        if (_dashCoroutine != null)
        {
            _context.StopCoroutine(_dashCoroutine);
        }
    }

    private void StartReposition()
    {
        _tries = 0;
        _newPosition = GetNewPosition();
        SetPath(_newPosition);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        if (_context.NavMeshAgent.remainingDistance < 0.5f)
        {
            if (_context.AimComponent.CanHitPlayer())
            {
                _superstate.ChangeSubState(null);
            }
            else
            {
                //Reposition again
                StartReposition();
            }
        }
    }

    private void SetPath(Vector3 destination)
    {
        _context.NavMeshAgent.SetDestination(destination);
    }

    private Vector3 GetNewPosition()
    {
        if (_tries >= _maxTries)
        {
            _context.ChangeState(_context.States.IdleState);
            return _context.transform.position;
        }
        _tries++;
        Vector3 playerPosition = _context.Player.transform.position;
        Vector3 enemyPosition = _context.transform.position;

        Vector3 direction = playerPosition - enemyPosition;

        // Chose a random point on the circle of the enemy side
        //Get angle from enemy to player
        float angleToPlayer = Mathf.Atan2(direction.z, direction.x);

        float angle = Random.Range(angleToPlayer - 45, angleToPlayer + 45);
        float range = Random.Range(_context.AttackRange * 0.2f, _context.AttackRange * 0.9f);

        Vector3 newPositionOffset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * range;

        // Add the offset to the player position
        Vector3 newPosition = playerPosition + newPositionOffset;

        // Check if has a path to the new position
        UnityEngine.AI.NavMeshPath path = new UnityEngine.AI.NavMeshPath();
        if (_context.NavMeshAgent.CalculatePath(newPosition, path))
        {
            return newPosition;
        }
        else
        {
            // If not, try again
            return GetNewPosition();
        }
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
