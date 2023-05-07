using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RangedAttackState : EnemyAttackState
{
    private Coroutine _attackCoroutine;
    private new RangedEnemy _context;

    public RangedAttackState(RangedEnemy enemy)
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
        base.StateUpdate();
        FacePlayer();
        if (_substate != null)
        {
            _substate.StateUpdate();
        }
    }

    private IEnumerator AttackCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_context.AttackSpeed);
            if (!(_substate is RangedRepositionState))
            {
                Shoot();
            }
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
        else
        {
            ChangeSubState(new RangedRepositionState(_context));
        }
    }

    private void FacePlayer()
    {
        Vector3 playerPosition = _context.Player.transform.position;
        playerPosition.y = _context.transform.position.y;
        _context.transform.LookAt(playerPosition);
    }
}
