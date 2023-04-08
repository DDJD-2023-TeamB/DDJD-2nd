using UnityEngine;
using System.Collections;

public class AimingState : GenericState
{
    private Player _context;
    private int _cameraPriorityBoost = 10;
    private string _lastAnimTrigger;
    public AimingState(StateContext context, GenericState superState) : base(context, superState)
    { 
        _context = (Player)context;
    }

    public override void Enter()
    {
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
        _context.Input.OnLeftShootKeydown -= OnLeftShootKeyDown;
        _context.Input.OnRightShootKeydown -= OnRightShootKeyDown;

        _context.Input.OnLeftShootKeyup -= OnLeftShootKeyup;
        _context.Input.OnRightShootKeyup -= OnRightShootKeyup;
    }

    public override bool CanChangeState(GenericState state)
    {
        return true;
    }

    public override void StateUpdate()
    {
        if(_lastAnimTrigger != null){
            //_context.Animator.ResetTrigger(_lastAnimTrigger);
            _lastAnimTrigger = null;
        }
        if(!_context.Input.IsAiming){
            _superstate.ChangeSubState(null);
        }
        //Attack();
         
        
    }

    private void Attack(){
        if(_context.Input.IsLeftShooting){
            //LeftShoot();
        }
        if(_context.Input.IsRightShooting){
            //RightShoot();
        }
    }

    private void OnShootKeyDown(Skill skill, GameObject spell, string animationTrigger){
        if(_context.PlayerSkills.IsSkillOnCooldown(skill)){
            return;
        }
        Vector3 origin = spell.transform.position;
        switch(skill.ShootType){
            case ShootType.Instant:
                _context.PlayerSkills.StartSkillCooldown(skill);
                Vector3 direction = _context.AimComponent.GetAimDirection(origin);
                _context.Shooter.Shoot(spell, direction);
                _context.Animator.SetTrigger(animationTrigger);
                _lastAnimTrigger = animationTrigger;
                break;
            case ShootType.Charge:
                _context.Animator.SetTrigger(animationTrigger);
                _lastAnimTrigger = animationTrigger;
                break;
            case ShootType.Hold:
                // TODO
                break;
        }
    }

    private void OnShootKeyUp(Skill skill, GameObject spell, string animationTrigger){
        if(_context.PlayerSkills.IsSkillOnCooldown(skill)){
            return;
        }
        Vector3 origin = spell.transform.position;
        switch(skill.ShootType){
            case ShootType.Instant:
                // DO nothing
                break;
            case ShootType.Charge:
                Vector3 direction = _context.AimComponent.GetAimDirection(origin);
                _context.Shooter.Shoot(spell, direction);
                _context.Animator.SetTrigger(animationTrigger);
                _lastAnimTrigger = animationTrigger;
                break;
            case ShootType.Hold:
                // TODO
                break;
        }
    }

    public static bool GiveSubState (GenericState state, StateContext context){
        if(!(context is Player)){
            return false;
        }
        Player player = (Player)context;
        if(!(state.Substate is AimingState) && player.Input.IsAiming){
            state.ChangeSubState(player.Factory.Aiming(state));
            return true;
        }
        return false;
    }


    
    void OnLeftShootKeyDown(){
        Skill skill = _context.PlayerSkills.LeftSkill;
        GameObject spell = _context.Shooter.CreateLeftSpell(skill, _context.LeftHand.transform);
        OnShootKeyDown(skill, spell, "LeftShoot");   
    }

    void OnRightShootKeyDown(){
        Skill skill = _context.PlayerSkills.RightSkill;
        GameObject spell = _context.Shooter.CreateRightSpell(skill, _context.RightHand.transform);
        OnShootKeyDown(skill, spell, "RightShoot");

    }

    void OnLeftShootKeyup(){
        Skill skill = _context.PlayerSkills.LeftSkill;
        GameObject spell = _context.Shooter.LeftSpell;
        OnShootKeyUp(skill, spell, "LeftShoot");

    }

    void OnRightShootKeyup(){
        Skill skill = _context.PlayerSkills.RightSkill;
        GameObject spell = _context.Shooter.RightSpell;
        OnShootKeyUp(skill, spell, "RightShoot");
    }


    
}