using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyRandomPatrolState : EnemyIdleState
{
    private float _previousSpeed;
    private Coroutine _samplePositionCoroutine;

    public EnemyRandomPatrolState(BasicEnemy enemy)
        : base(enemy) { }

    public override void Enter()
    {
        base.Enter();
        _context.NavMeshAgent.enabled = true;
        _context.Rigidbody.isKinematic = true;
        _previousSpeed = _context.NavMeshAgent.speed;
        _context.NavMeshAgent.speed = _previousSpeed / 4.0f;
        GoToRandom();
    }

    public override void Exit()
    {
        base.Exit();
        _context.NavMeshAgent.speed = _previousSpeed;
        if (_samplePositionCoroutine != null)
        {
            _context.StopCoroutine(_samplePositionCoroutine);
        }
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        _context.Animator.SetFloat(
            _context.ForwardSpeedHash,
            _context.NavMeshAgent.velocity.magnitude / _previousSpeed
        );
        if (_context.NavMeshAgent.remainingDistance < 0.5f)
        {
            if (_samplePositionCoroutine == null)
            {
                _samplePositionCoroutine = _context.StartCoroutine(SampleAnotherPosition());
            }
        }
    }

    private void GoToRandom()
    {
        Vector3 randomPoint = _context.transform.position + Random.insideUnitSphere * 5.0f;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 5.0f, NavMesh.AllAreas))
        {
            Vector3 positionToGo = hit.position;
            _context.NavMeshAgent.SetDestination(positionToGo);
        }
    }

    private IEnumerator SampleAnotherPosition()
    {
        yield return new WaitForSeconds(Random.Range(1.0f, 5.0f));
        GoToRandom();
        _samplePositionCoroutine = null;
    }
}
