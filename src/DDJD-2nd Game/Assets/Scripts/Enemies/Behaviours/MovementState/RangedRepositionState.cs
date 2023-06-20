using UnityEngine;
using System.Collections;

public class RangedRepositionState : EnemyMovingState
{
    protected new RangedEnemy _context;

    private int _tries = 0;
    private int _maxTries = 5;

    private Vector3 _newPosition;

    private Coroutine _dashCoroutine;

    private float _repositionRange;

    public RangedRepositionState(RangedEnemy enemy, float repositionRange = 0.0f)
        : base(enemy)
    {
        _context = enemy;
        _repositionRange = repositionRange;
    }

    public override void Enter()
    {
        base.Enter();
        StartReposition();
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
                _context.ChangeState(_context.States.AttackState);
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
            Debug.Log("Couldn't find a new position to reposition, going to attack");
            _context.ChangeState(_context.States.AttackState);
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
        float range;
        Vector3 targetPosition;
        if (_repositionRange > 0)
        {
            range = _repositionRange;
            targetPosition = enemyPosition;
        }
        else
        {
            range = Random.Range(_context.AttackRange * 0.2f, _context.AttackRange * 0.9f);
            targetPosition = playerPosition;
        }

        Vector3 newPositionOffset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * range;

        // Add the offset to the player position
        Vector3 newPosition = targetPosition + newPositionOffset;

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
}
