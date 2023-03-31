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
        _attackCD = 0.2f;
        _timeUntilNextAttack = 0f;
    }

    private float _attackCD;
    private float _timeUntilNextAttack;

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
        if(_timeUntilNextAttack > 0f){
            _timeUntilNextAttack -= Time.deltaTime;
        }
        if(!_context.Input.IsAiming){
            _superstate.ChangeSubState(null);
        }

        if(_timeUntilNextAttack > 0f){
            return;
        }
        Attack();
         
        
    }

    private void Attack(){
        if(_context.Input.IsLeftShooting && _timeUntilNextAttack <= 0f){
            LeftShoot();
        }
        if(_context.Input.IsRightShooting && _timeUntilNextAttack <= 0f){
            RightShoot();
        }
        if(_context.Input.IsWaveAttacking){
            WaveAttack();
        }
    }

    private void LeftShoot(){

        //TODO:: removed repeated code
        _timeUntilNextAttack = _attackCD;
        Vector3 position = _context.LeftHand.transform.position;
        Vector3 direction = _context.AimComponent.GetAimDirection(position);
        _context.Shooter.LeftSpell = _context.EquippedSkills.LeftSkill;
        _context.Shooter.LeftShoot(position, direction);
        _context.Animator.SetTrigger("LeftShoot");
        _lastAnimTrigger = "LeftShoot";
    }

    private void RightShoot(){
        _timeUntilNextAttack = _attackCD;
        Vector3 position = _context.RightHand.transform.position;
        Vector3 direction = _context.AimComponent.GetAimDirection(position);
        _context.Shooter.RightSpell = _context.EquippedSkills.RightSkill;
        _context.Shooter.RightShoot(position, direction);
        _context.Animator.SetTrigger("RightShoot");
        _lastAnimTrigger = "RightShoot";
    }


    private void WaveAttack(){
        _timeUntilNextAttack = _attackCD;
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