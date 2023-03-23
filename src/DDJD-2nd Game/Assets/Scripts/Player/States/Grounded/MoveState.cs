using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class MoveState : GenericState {
    private Player _context;

    public MoveState(StateContext context, GenericState superState) : base(context, superState)
    {
        _context = (Player)context;
    }

    public override void Enter()
    {
        

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
        if(CheckCurrentStates()){
            return;
        }

        Vector2 moveInput = _context.Input.MoveInput;
        if(moveInput == Vector2.zero){
            Decelerate();
        }

        if(moveInput != Vector2.zero){
            Accelerate(moveInput);
        }
        _context.Animator.SetFloat("ForwardSpeed", Vector3.Dot(_context.Rigidbody.velocity, _context.transform.forward));
        _context.Animator.SetFloat("RightSpeed", Vector3.Dot(_context.Rigidbody.velocity, _context.transform.right));
    }

    
    // Check state and substate, return true if state is changed
    private bool CheckCurrentStates(){
        if(!AimingState.GiveSubState(this, _context)){
            NotAimingState.GiveSubState(this, _context);
        }
            
        

        if(_context.Input.MoveInput == Vector2.zero){
            if(_context.Rigidbody.velocity.magnitude < 0.1f){
                _superstate.ChangeSubState(_context.Factory.Idle(_superstate));
                return true;
            }
        }
        return false;
    }

    private void Decelerate(){
        Vector3 velocity = _context.Rigidbody.velocity;
        if(velocity.magnitude > 0.1f){
            _context.Rigidbody.AddForce(-velocity.normalized * _context.Acceleration * Time.deltaTime, ForceMode.Acceleration);
        } else {
            velocity = Vector3.zero;
        }
    }

    private void Accelerate(Vector2 moveInput){
        Vector3 velocity = _context.Rigidbody.velocity;
        Vector3 moveDirection = _context.transform.forward * moveInput.y + _context.transform.right * moveInput.x;
        if(velocity.magnitude < _context.MaxSpeed){
            _context.Rigidbody.AddForce(moveDirection * _context.Acceleration * Time.deltaTime * 1000, ForceMode.Acceleration);
        }
        
    }
}