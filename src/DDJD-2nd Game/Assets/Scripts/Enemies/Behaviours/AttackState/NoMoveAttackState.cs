using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NoMoveAttackState : EnemyAttackState
{
    private Coroutine _attackCoroutine;
    private new RangedEnemy _context;

    public NoMoveAttackState(RangedEnemy enemy)
        : base(enemy)
    {
        _context = enemy;
    }

    protected override void Attack() { }

    protected override void Move() { }

    public override void Enter()
    {
        base.Enter();
        _context.Animator.SetBool("IsAiming", true);
        _context.AimComponent.StartAim();
        _attackCoroutine = _context.StartCoroutine(AttackCoroutine());
    }

    public override void Exit()
    {
        base.Exit();
        _context.Animator.SetBool("IsAiming", false);
        _context.AimComponent.StopAim();
        if (_attackCoroutine != null)
        {
            _context.StopCoroutine(_attackCoroutine);
        }

        _context.Shooter.CancelShots();
    }

    public override void StateUpdate()
    {
        if (!IsInAttackRange())
        {
            _context.ChangeState(_context.States.IdleState);
            return;
        }

        Attack();
        Move();
        FacePlayer();
    }

    private IEnumerator AttackCoroutine()
    {
        //Wait one frame
        yield return 0;
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(_context.EnemySkills.AttackSpeed);
        }
    }

    private void Shoot()
    {
        AimedSkill aimedSkill;
        Transform origin;
        bool isLeft;
        bool canAttack = _context.Shooter.ChooseSpellToUse(out aimedSkill, out origin, out isLeft);
        if (canAttack)
        {
            _context.Shooter.Shoot(aimedSkill, origin, isLeft);
        }
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
