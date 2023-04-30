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
            GameObject.Destroy(spell);
            return;
        }
        Vector3 origin = spell.transform.position;
        Vector3 direction;
        switch (skill.SkillStats.CastType)
        {
            case CastType.Instant:
                _context.PlayerSkills.StartSkillCooldown(skill);
                direction = _context.AimComponent.GetAimDirection(origin);
                _context.Shooter.Shoot(spell, direction);
                _context.Animator.SetTrigger(animationTrigger);
                _lastAnimTrigger = animationTrigger;
                break;
            case CastType.Charge:
                _context.Animator.SetTrigger(animationTrigger);
                _lastAnimTrigger = animationTrigger;
                break;
            case CastType.Hold:
                _context.PlayerSkills.StartSkillCooldown(skill);
                _lastAnimTrigger = animationTrigger;
                direction = _context.AimComponent.GetAimDirection(origin);
                _context.Shooter.Shoot(spell, direction);
                _context.Animator.SetTrigger(animationTrigger);
                break;
        }
    }

    private void OnShootKeyUp(
        AimedSkill skill,
        GameObject spell,
        string animationTrigger,
        bool isLeft
    )
    {
        if (spell == null)
        {
            //Was destroyed
            return;
        }
        Vector3 direction;
        Vector3 origin = spell.transform.position;
        switch (skill.SkillStats.CastType)
        {
            case CastType.Instant:
                // DO nothing
                break;
            case CastType.Charge:
                direction = _context.AimComponent.GetAimDirection(origin);
                _context.Shooter.Shoot(spell, direction);
                _context.Animator.SetTrigger(animationTrigger);
                _lastAnimTrigger = animationTrigger;
                _context.PlayerSkills.StartSkillCooldown(skill);
                break;
            case CastType.Hold:
                if (isLeft)
                {
                    _context.Shooter.CancelLeftShoot();
                }
                else
                {
                    _context.Shooter.CancelRightShoot();
                }

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
        OnShootKeyUp(skill, spell, "LeftShoot", true);
    }

    void OnRightShootKeyup()
    {
        AimedSkill skill = _context.PlayerSkills.RightSkill;
        GameObject spell = _context.Shooter.RightSpell;
        OnShootKeyUp(skill, spell, "RightShoot", false);
    }
}
