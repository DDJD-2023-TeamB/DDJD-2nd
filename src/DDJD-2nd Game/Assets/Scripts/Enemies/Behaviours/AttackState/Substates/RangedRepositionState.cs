using UnityEngine;

public class RangedRepositionState : GenericState
{
    protected RangedEnemy _context;

    private int _tries = 0;
    private int _maxTries = 5;

    private Vector3 _newPosition;

    public RangedRepositionState(RangedEnemy enemy)
        : base(enemy)
    {
        _context = enemy;
    }

    public override void Enter()
    {
        _context.Rigidbody.isKinematic = true;
        _context.NavMeshAgent.enabled = true;
        StartReposition();
    }

    public override void Exit()
    {
        _context.Rigidbody.isKinematic = false;
        _context.NavMeshAgent.ResetPath();
        _context.NavMeshAgent.enabled = false;
    }

    private void StartReposition()
    {
        _tries = 0;
        _newPosition = GetNewPosition();
        SetPath(_newPosition);
    }

    public override void StateUpdate()
    {
        if (_context.NavMeshAgent.remainingDistance < 0.5f)
        {
            if (_context.AimComponent.CanSeePlayer())
            {
                Debug.Log(_superstate.ChangeSubState(null));
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

        // Chose a random point on the circle
        float angle = Random.Range(0, 2 * Mathf.PI);
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
}
