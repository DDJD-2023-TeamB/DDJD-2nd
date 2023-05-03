using UnityEngine;
using System.Collections;

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
    }

    private IEnumerator AttackCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_context.AttackSpeed);
            Shooter shooter = _context.Shooter;
            AimedSkill aimedSkill = _context.EnemySkills.LeftSkill;
            GameObject spell = _context.Shooter.CreateLeftSpell(
                aimedSkill,
                _context.LeftSpellOrigin.transform
            );
            Vector3 shotDirection = GetShotDirection(shooter.transform.position);
            shooter.Shoot(spell, shotDirection, true);
        }
    }

    private Vector3 GetShotDirection(Vector3 spellOrigin)
    {
        Vector3 direction = _context.Player.transform.position - spellOrigin;
        return direction;
    }
}
