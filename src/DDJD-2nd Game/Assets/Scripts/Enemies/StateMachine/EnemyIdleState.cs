using UnityEngine;
using System.Collections;
using System;

public class EnemyIdleState : GenericState
{
    protected BasicEnemy _context;

    private Coroutine _loopRoutine;

    public EnemyIdleState(BasicEnemy enemy)
        : base(enemy)
    {
        _context = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        _loopRoutine = _context.StartCoroutine(DetectPlayerLoop());
        _context.NoiseListener.OnNoiseHeard += OnNoiseHeard;
        _context.OnDamageTaken += OnDamageTaken;
        Action<EnemyMessage> playerSightedAction = OnPlayerSightedMessage;
        _context.EnemyCommunicator.SetMessageAction(
            typeof(PlayerSightedMessage),
            playerSightedAction
        );
    }

    private IEnumerator DetectPlayerLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if (IsInAggroRange())
            {
                if (_context.LineOfSight.CanSeePlayer() || _context.NoiseListener.CanHearPlayer())
                {
                    _context.StartCoroutine(WarnNearbyEnemies());
                    _context.ChangeState(_context.States.ChaseState);
                }
            }
        }
    }

    public override void StateUpdate() { }

    public override void Exit()
    {
        base.Exit();
        _context.StopCoroutine(_loopRoutine);
        _context.NoiseListener.OnNoiseHeard -= OnNoiseHeard;
        _context.OnDamageTaken -= OnDamageTaken;
        _context.EnemyCommunicator.DeleteAction(
            typeof(PlayerSightedMessage),
            OnPlayerSightedMessage
        );
    }

    private void OnNoiseHeard(Vector3 position)
    {
        Vector3 direction = position - _context.transform.position;
        direction.y = 0;
        ChangeSubState(new EnemyLookAtState(_context, direction));
    }

    public override bool CanChangeState(GenericState state)
    {
        return true;
    }

    protected bool IsInAggroRange()
    {
        return Vector3.Distance(_context.transform.position, _context.Player.transform.position)
            <= _context.AggroRange;
    }

    private void OnDamageTaken()
    {
        _context.ChangeState(_context.States.ChaseState);
    }

    private void OnPlayerSightedMessage(EnemyMessage message)
    {
        _context.ChangeState(_context.States.ChaseState);
    }

    private IEnumerator WarnNearbyEnemies()
    {
        yield return new WaitForSeconds(1.5f);
        Collider[] colliders = Physics.OverlapSphere(
            _context.transform.position,
            _context.AggroRange,
            LayerMask.GetMask("Enemy")
        );
        PlayerSightedMessage message = new PlayerSightedMessage(_context.transform.position);
        foreach (Collider collider in colliders)
        {
            EnemyCommunicator communicator = collider.GetComponent<EnemyCommunicator>();
            float timeToWait =
                Vector3.Distance(_context.transform.position, collider.transform.position) / 10.0f;
            yield return new WaitForSeconds(timeToWait);
            communicator?.ReceiveMessage(message);
        }
    }
}
