using UnityEngine;

public class EnemyLookAtState : GenericState
{
    protected BasicEnemy _context;

    Quaternion _desiredRotation;
    private float _turnSpeed = 10f;

    public EnemyLookAtState(BasicEnemy enemy, Vector3 desiredDirection)
        : base(enemy)
    {
        _context = enemy;
        _desiredRotation = Quaternion.LookRotation(desiredDirection);
    }

    public override void Enter() { }

    public override void Exit() { }

    public override void StateUpdate()
    {
        _context.transform.rotation = Quaternion.Slerp(
            _context.transform.rotation,
            _desiredRotation,
            Time.deltaTime * _turnSpeed
        );

        if (_context.transform.rotation == _desiredRotation)
        {
            _superstate.ChangeSubState(null);
        }
    }
}
