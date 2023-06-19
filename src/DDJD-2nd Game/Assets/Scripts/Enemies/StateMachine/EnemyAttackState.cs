using UnityEngine;
using UnityEditor;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(BasicEnemy enemy)
        : base(enemy) { }

    public override void Enter()
    {
        base.Enter();
        _context.NavMeshAgent.enabled = false;
        _context.Animator.enabled = true;
        _context.Animator.SetBool("IsAiming", true);
        _context.Animator.SetFloat(_context.ForwardSpeedHash, 0);
        _context.Animator.SetFloat(_context.RightSpeedHash, 0);
    }

    public override void StateUpdate()
    {
        if (!IsInAttackRange())
        {
            _context.ChangeState(_context.States.ChaseState);
            return;
        }

        Attack();
        Move();
    }

    protected virtual void Attack() { }

    protected virtual void Move() { }

    public override void Exit()
    {
        base.Exit();
        _context.Animator.SetBool("IsAiming", false);
    }

    protected bool IsInAttackRange()
    {
        return Vector3.Distance(_context.transform.position, _context.Player.transform.position)
            <= _context.AttackRange;
    }
}
