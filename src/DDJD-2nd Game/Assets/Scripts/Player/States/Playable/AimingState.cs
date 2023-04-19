using UnityEngine;
using System.Collections;

public class AimingState : MovableState
{
    private int _cameraPriorityBoost = 10;
    private string _lastAnimTrigger;

    public AimingState(StateContext context, GenericState superState)
        : base(context, superState)
    {
        _context = (Player)context;
    }

    public override void Enter()
    {
        base.Enter();
        _context.AimCamera.Priority = 5 + _cameraPriorityBoost;
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

        _context.AimCamera.Priority = 5;
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
            //_context.Animator.ResetTrigger(_lastAnimTrigger);
            _lastAnimTrigger = null;
        }
        if (!_context.Input.IsAiming)
        {
            _superstate.ChangeSubState(null);
        }
    }

    private void OnShootKeyDown(AimedSkill skill, GameObject spell, string animationTrigger)
    {
        if (_context.PlayerSkills.IsSkillOnCooldown(skill))
        {
            return;
        }
        Vector3 origin = spell.transform.position;
        switch (skill.Stats.CastType)
        {
            case CastType.Instant:
                _context.PlayerSkills.StartSkillCooldown(skill);
                Vector3 direction = _context.AimComponent.GetAimDirection(origin);
                _context.Shooter.Shoot(spell, direction);
                _context.Animator.SetTrigger(animationTrigger);
                _lastAnimTrigger = animationTrigger;
                break;
            case CastType.Charge:
                _context.Animator.SetTrigger(animationTrigger);
                _lastAnimTrigger = animationTrigger;
                break;
            case CastType.Hold:
                // TODO
                break;
        }
    }

    private void OnShootKeyUp(AimedSkill skill, GameObject spell, string animationTrigger)
    {
        if (_context.PlayerSkills.IsSkillOnCooldown(skill))
        {
            return;
        }
        switch (skill.Stats.CastType)
        {
            case CastType.Instant:
                // DO nothing
                break;
            case CastType.Charge:
                Vector3 origin = spell.transform.position;
                Vector3 direction = _context.AimComponent.GetAimDirection(origin);
                _context.Shooter.Shoot(spell, direction);
                _context.Animator.SetTrigger(animationTrigger);
                _lastAnimTrigger = animationTrigger;
                break;
            case CastType.Hold:
                // TODO
                break;
        }
    }

    void OnLeftShootKeyDown()
    {
        AimedSkill skill = _context.PlayerSkills.LeftSkill;
        GameObject spell = _context.Shooter.CreateLeftSpell(skill, _context.LeftHand.transform);
        OnShootKeyDown(skill, spell, "LeftShoot");
    }

    void OnRightShootKeyDown()
    {
        AimedSkill skill = _context.PlayerSkills.RightSkill;
        GameObject spell = _context.Shooter.CreateRightSpell(skill, _context.RightHand.transform);
        OnShootKeyDown(skill, spell, "RightShoot");
    }

    void OnLeftShootKeyup()
    {
        AimedSkill skill = _context.PlayerSkills.LeftSkill;
        GameObject spell = _context.Shooter.LeftSpell;
        OnShootKeyUp(skill, spell, "LeftShoot");
    }

    void OnRightShootKeyup()
    {
        AimedSkill skill = _context.PlayerSkills.RightSkill;
        GameObject spell = _context.Shooter.RightSpell;
        OnShootKeyUp(skill, spell, "RightShoot");
    }
}
