using UnityEngine;
using System.Collections;

public class EnemyMoveToState : EnemyState
{
    private Vector3 _position;
    private Coroutine _checkDestinationCoroutine;

    public EnemyMoveToState(BasicEnemy context, Vector3 position)
        : base(context)
    {
        _position = position;
    }

    public override void Enter()
    {
        base.Enter();
        _context.NavMeshAgent.enabled = true;
        _context.Rigidbody.isKinematic = true;
        _context.NavMeshAgent.ResetPath();
        _context.NavMeshAgent.SetDestination(_position);
        _checkDestinationCoroutine = _context.StartCoroutine(CheckDestination());
        _context.OnDamageTaken += OnDamageTaken;
        _context.EnemyCommunicator.SetMessageAction(
            typeof(PlayerSightedMessage),
            OnPlayerSightedMessage
        );
    }

    public override void Exit()
    {
        base.Exit();
        _context.OnDamageTaken -= OnDamageTaken;
        if (_checkDestinationCoroutine != null)
        {
            _context.StopCoroutine(_checkDestinationCoroutine);
        }
        _context.EnemyCommunicator.DeleteAction(
            typeof(PlayerSightedMessage),
            OnPlayerSightedMessage
        );
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

    private IEnumerator CheckDestination()
    {
        while (true)
        {
            if (Vector3.Distance(_position, _context.transform.position) <= 0.5f)
            {
                _context.NavMeshAgent.enabled = false;
                _context.Rigidbody.isKinematic = false;
                _context.ChangeState(_context.States.IdleState);
                yield break;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void OnDamageTaken()
    {
        _context.ChangeState(_context.States.ChaseState);
    }

    private void OnPlayerSightedMessage(EnemyMessage message)
    {
        _context.ChangeState(_context.States.ChaseState);
    }
}
