using UnityEngine;
using System.Collections;

public class EnemyWaitState : EnemyState
{
    protected new RangedEnemy _context;
    private Coroutine _waitCoroutine;

    private float _timeToWait;

    public EnemyWaitState(RangedEnemy enemy, float time = 0.0f)
        : base(enemy)
    {
        _context = enemy;
        _timeToWait = time;
    }

    public override void Enter()
    {
        base.Enter();
        _waitCoroutine = _context.StartCoroutine(Wait());
    }

    public override void Exit()
    {
        base.Exit();
        if (_waitCoroutine != null)
        {
            _context.StopCoroutine(_waitCoroutine);
        }
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        FacePlayer();
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(_timeToWait);
        _context.ChangeState(_context.States.AttackState);
    }

    private void FacePlayer()
    {
        Vector3 playerPosition = _context.Player.transform.position;
        playerPosition.y = _context.transform.position.y;
        Quaternion rotation = Quaternion.LookRotation(playerPosition - _context.transform.position);
        _context.transform.rotation = Quaternion.Slerp(
            _context.transform.rotation,
            rotation,
            Time.deltaTime * 7.5f
        );
    }
}
