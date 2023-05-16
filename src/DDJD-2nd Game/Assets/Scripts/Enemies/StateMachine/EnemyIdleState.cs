using UnityEngine;
using System.Collections;

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
        _loopRoutine = _context.StartCoroutine(DetectPlayerLoop());
        _context.NoiseListener.OnNoiseHeard += OnNoiseHeard;
        _context.OnDamageTaken += OnDamageTaken;
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
}
