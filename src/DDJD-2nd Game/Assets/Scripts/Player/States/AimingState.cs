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


        
        
    }

    public override void Exit()
    {
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
        Attack();
         
        
    }

    private void Attack(){
        if(_context.Input.IsLeftShooting){
            LeftShoot();
        }
        if(_context.Input.IsRightShooting){
            RightShoot();
        }
    }

    private void LeftShoot(){
        Skill skill = _context.PlayerSkills.LeftSkill;
        Vector3 position = _context.LeftHand.transform.position;
        Shoot(skill, position, "LeftShoot");
    }

    private void RightShoot(){
        Skill skill = _context.PlayerSkills.RightSkill;
        Vector3 position = _context.RightHand.transform.position;
        Shoot(skill, position, "RightShoot");
    }

    private void Shoot(Skill skill, Vector3 origin, string animationTrigger){
        if(_context.PlayerSkills.IsSkillOnCooldown(skill)){
            return;
        }
        _context.PlayerSkills.StartSkillCooldown(skill);
        Vector3 direction = _context.AimComponent.GetAimDirection(origin);
        _context.Shooter.RightSpell = skill;
        _context.Shooter.RightShoot(origin, direction);
        _context.Animator.SetTrigger(animationTrigger);
        _lastAnimTrigger = animationTrigger;
    }


    private void WaveAttack(){
        _context.Animator.SetTrigger("WaveAttack");
        _lastAnimTrigger = "WaveAttack";
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
}