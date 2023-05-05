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
            AimedSkill aimedSkill = _context.EnemySkills.LeftSkill;
            Shoot(aimedSkill, _context.LeftSpellOrigin);
        }
    }

    private void Shoot(AimedSkill aimedSkill, GameObject origin)
    {
        GameObject spell = _context.Shooter.CreateLeftSpell(aimedSkill, origin.transform);
        Vector3 shotDirection = GetShotDirection(origin.transform.position);
        _context.Shooter.Shoot(spell, shotDirection, true);
    }

    private Vector3 GetShotDirection(Vector3 spellOrigin)
    {
        Vector3 playerPosition = _context.Player.transform.position;
        playerPosition.y += 0.8f;
        Vector3 direction = playerPosition - spellOrigin;
        return direction;
    }
}
