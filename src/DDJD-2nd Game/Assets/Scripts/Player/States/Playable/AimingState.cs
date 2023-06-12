using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;

public class AimingState : MovableState
{
    private string _lastAnimTrigger;

    private List<SkillComponent> _skillsToAim = new List<SkillComponent>();

    public AimingState(StateContext context, GenericState superState)
        : base(context, superState)
    {
        _context = (Player)context;
    }

    public override void Enter()
    {
        base.Enter();
        _context.CameraController.SetAimCamera(true);
        _context.Animator.SetBool("IsAiming", true);
        _context.AimComponent.StartAim();

        _context.Input.OnLeftShootKeydown += OnLeftShootKeyDown;
        _context.Input.OnRightShootKeydown += OnRightShootKeyDown;

        _context.Input.OnLeftShootKeyup += OnLeftShootKeyup;
        _context.Input.OnRightShootKeyup += OnRightShootKeyup;
    }

    public override void Exit()
    {
        base.Exit();
        _context.Input.OnLeftShootKeydown -= OnLeftShootKeyDown;
        _context.Input.OnRightShootKeydown -= OnRightShootKeyDown;

        _context.Input.OnLeftShootKeyup -= OnLeftShootKeyup;
        _context.Input.OnRightShootKeyup -= OnRightShootKeyup;

        _context.CameraController.SetAimCamera(false);
        _context.Animator.SetBool("IsAiming", false);
        _context.AimComponent.StopAim();

        _context.Shooter.CancelLeftShoot();
        _context.Shooter.CancelRightShoot();
    }

    public override bool CanChangeState(GenericState state)
    {
        if (!base.CanChangeState(state))
        {
            return false;
        }
        return true;
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        if (_lastAnimTrigger != null)
        {
            _lastAnimTrigger = null;
        }
        if (!_context.Input.IsAiming)
        {
            _superstate.ChangeSubState(null);
        }

        foreach (SkillComponent skill in _skillsToAim)
        {
            if (skill == null)
            {
                continue;
            }
            AimSpell(skill);
        }
    }

    private void OnShootKeyDown(AimedSkill skill, bool isLeft)
    {
        if (_context.PlayerSkills.IsSkillOnCooldown(skill))
        {
            return;
        }

        GameObject spell;
        if (skill.SkillStats.CastType == CastType.Spawn)
        {
            spell = _context.ObjectSpawner.CreatePreviewObject(
                (SpawnSkill)skill,
                skill.SpellPrefab.transform.rotation
            );
        }
        else if (
            skill.SkillStats.CastType == CastType.Instant
            && _context.CharacterStatus.Mana < skill.SkillStats.ManaCost
        )
        {
            return;
        }
        else if (_context.CharacterStatus.Mana > 0 || skill.SkillStats.ManaCost == 0) // Prevent charge and hold spells from being created without no mana
        {
            spell = isLeft
                ? _context.Shooter.CreateLeftSpell(skill, _context.LeftHand.transform)
                : _context.Shooter.CreateRightSpell(skill, _context.RightHand.transform);
        }
        else
            return;

        string animationTrigger = GetAnimationTrigger(isLeft);
        Vector3 origin = spell.transform.position;
        Vector3 direction;
        bool success = true;
        switch (skill.SkillStats.CastType)
        {
            case CastType.Instant:
                direction = _context.AimComponent.GetAimDirection(origin);
                success = _context.Shooter.Shoot(spell, direction, true, skill.SkillStats.ManaCost);
                if (success)
                {
                    _context.Animator.SetTrigger(animationTrigger);
                    _context.PlayerSkills.StartSkillCooldown(skill);
                    _lastAnimTrigger = animationTrigger;
                }
                break;
            case CastType.Charge:
                _context.Animator.SetTrigger(animationTrigger);
                _lastAnimTrigger = animationTrigger;
                break;
            case CastType.Hold:
                _lastAnimTrigger = animationTrigger;
                direction = _context.AimComponent.GetAimDirection(origin);
                success = _context.Shooter.Shoot(
                    spell,
                    direction,
                    false,
                    skill.SkillStats.ManaCost
                );
                if (success)
                {
                    _context.PlayerSkills.StartSkillCooldown(skill);
                    _context.Animator.SetTrigger(animationTrigger);
                    _skillsToAim.Add(spell.GetComponent<SkillComponent>());
                }

                break;
        }

        if (!success)
        {
            GameObject.Destroy(spell);
        }
    }

    private void OnShootKeyUp(AimedSkill skill, bool isLeft)
    {
        GameObject spell;
        if (skill.SkillStats.CastType == CastType.Spawn)
        {
            spell = _context.ObjectSpawner.PreviewObject;
        }
        else
        {
            spell = isLeft ? _context.Shooter.LeftSpell : _context.Shooter.RightSpell;
        }

        if (spell == null)
        {
            // Was destroyed or wasn't spawned
            return;
        }

        bool success = true;
        string animationTrigger = GetAnimationTrigger(isLeft);
        switch (skill.SkillStats.CastType)
        {
            case CastType.Charge:
                Vector3 origin = spell.transform.position;
                Vector3 direction = _context.AimComponent.GetAimDirection(origin);
                ChargeComponent chargeComponent = spell.GetComponent<ChargeComponent>();
                success = _context.Shooter.Shoot(
                    spell,
                    direction,
                    true,
                    chargeComponent.GetManaCost()
                );
                _context.Animator.SetTrigger(animationTrigger);
                _lastAnimTrigger = animationTrigger;
                if (success)
                {
                    _context.PlayerSkills.StartSkillCooldown(skill);
                }
                break;
            case CastType.Hold:
                SkillComponent skillComponent = spell.GetComponent<SkillComponent>();
                skillComponent.DestroySpell();
                _skillsToAim.Remove(skillComponent);
                if (isLeft)
                    _context.Shooter.CancelLeftShoot();
                else
                    _context.Shooter.CancelRightShoot();

                break;
            case CastType.Spawn:
                success = _context.ObjectSpawner.SpawnObject((SpawnSkill)skill);
                if (success)
                {
                    _context.PlayerSkills.StartSkillCooldown(skill);
                }
                break;
        }

        if (!success)
        {
            _context.PlayerSkills.StartSkillCooldown(skill);
        }
    }

    void OnLeftShootKeyDown()
    {
        AimedSkill skill = _context.PlayerSkills.LeftSkill;
        OnShootKeyDown(skill, true);
    }

    void OnRightShootKeyDown()
    {
        AimedSkill skill = _context.PlayerSkills.RightSkill;
        OnShootKeyDown(skill, false);
    }

    void OnLeftShootKeyup()
    {
        AimedSkill skill = _context.PlayerSkills.LeftSkill;
        GameObject spell = _context.Shooter.LeftSpell;
        OnShootKeyUp(skill, true);
    }

    void OnRightShootKeyup()
    {
        AimedSkill skill = _context.PlayerSkills.RightSkill;
        GameObject spell = _context.Shooter.RightSpell;
        OnShootKeyUp(skill, false);
    }

    private void AimSpell(SkillComponent spell)
    {
        if (spell == null)
        {
            return;
        }

        Vector3 origin = spell.transform.position;
        Vector3 direction = _context.AimComponent.GetAimDirection(origin);
        spell.Aim(direction);
    }

    private string GetAnimationTrigger(bool isLeft)
    {
        return isLeft ? "LeftShoot" : "RightShoot";
    }
}
