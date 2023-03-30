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
        if(_context.Input.IsShooting && _timeUntilNextAttack <= 0f){
            Shoot();
        }  
        if(_context.Input.IsWaveAttacking){
            WaveAttack();
        }
    }

    private void Shoot(){
        _timeUntilNextAttack = _attackCD;
        Vector3 position = _context.AimCamera.transform.position + _context.AimCamera.transform.forward * 10f;
        _context.Shooter.Shoot();
        _context.Animator.SetTrigger("Shoot");
        _lastAnimTrigger = "Shoot";
        
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