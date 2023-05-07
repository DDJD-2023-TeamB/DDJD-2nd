using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyShooter : Shooter
{
    private RangedEnemy _enemy;

    private List<Coroutine> _routines = new List<Coroutine>();

    protected override void Awake()
    {
        base.Awake();
        _enemy = GetComponent<RangedEnemy>();
    }

    public bool ChooseSpellToUse(out AimedSkill aimedSkill, out Transform origin, out bool isLeft)
    {
        // Choose which side to shoot
        Transform leftOrigin = _enemy.LeftSpellOrigin.transform;
        Transform rightOrigin = _enemy.RightSpellOrigin.transform;
        bool canShootLeft =
            _enemy.EnemySkills.LeftSkill != null && _leftSpell == null && CanHitPlayer(leftOrigin);
        isLeft = canShootLeft && Random.Range(0, 2) == 0;
        bool canShootRight = false;
        if (!isLeft)
        {
            canShootRight =
                _enemy.EnemySkills.RightSkill != null
                && _rightSpell == null
                && CanHitPlayer(rightOrigin);
            isLeft = !canShootRight;
        }
        bool canShoot = canShootLeft || canShootRight;

        //
        if (canShoot)
        {
            aimedSkill = isLeft ? _enemy.EnemySkills.LeftSkill : _enemy.EnemySkills.RightSkill;
            origin = isLeft ? leftOrigin : rightOrigin;
        }
        else
        {
            aimedSkill = null;
            origin = null;
        }
        return canShoot;
    }

    private bool CanHitPlayer(Transform origin)
    {
        Vector3 direction = GetShotDirection(origin.position);
        return _enemy.AimComponent.CanHitPlayer(origin.position, direction);
    }

    public void Shoot(AimedSkill aimedSkill, Transform origin, bool isLeft = true)
    {
        GameObject spell;
        if (isLeft)
            spell = CreateLeftSpell(aimedSkill, origin);
        else
            spell = CreateRightSpell(aimedSkill, origin);
        Vector3 shotDirection = GetShotDirection(origin.position);

        switch (aimedSkill.SkillStats.CastType)
        {
            case CastType.Instant:
                ShootInstant(aimedSkill, spell, isLeft, shotDirection);
                break;
            case CastType.Charge:
                ShootCharge(aimedSkill, spell, isLeft, shotDirection);
                break;
            case CastType.Hold:
                //To be implemented
                break;
        }
    }

    private void ShootInstant(
        AimedSkill aimedSkill,
        GameObject spell,
        bool isLeft,
        Vector3 shotDirection
    )
    {
        base.Shoot(spell, shotDirection, true);
    }

    private void ShootCharge(
        AimedSkill aimedSkill,
        GameObject spell,
        bool isLeft,
        Vector3 shotDirection
    )
    {
        StartCoroutineInList(_enemy.StartCoroutine(WaitForCharge(aimedSkill, spell)));
    }

    private IEnumerator WaitForCharge(AimedSkill aimedSkill, GameObject spell)
    {
        //GameObject spell = isLeft ? _enemy.Shooter.LeftSpell : _enemy.Shooter.RightSpell;
        yield return new WaitForSeconds(aimedSkill.SkillStats.MaxChargeTime + 0.5f);
        Vector3 direction = GetShotDirection(spell.transform.position);
        base.Shoot(spell, direction, true);
    }

    private Vector3 GetShotDirection(Vector3 spellOrigin)
    {
        Vector3 playerPosition = _enemy.Player.transform.position;
        playerPosition.y += 0.8f;
        Vector3 direction = playerPosition - spellOrigin;
        return direction;
    }

    //Waits for coroutine to finish and removes it from the list
    private IEnumerator StartCoroutineInList(Coroutine routine)
    {
        _routines.Add(routine);
        yield return routine;
        _routines.Remove(routine);
    }
}
