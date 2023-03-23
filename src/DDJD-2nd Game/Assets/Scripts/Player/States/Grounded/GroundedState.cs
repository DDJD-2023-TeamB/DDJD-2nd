using UnityEngine;
using System.Collections;

public class GroundedState : GenericState
{
    private Player _context;

    public GroundedState(StateContext context, GenericState superState) : base(context, superState)
    { 
        _context = (Player)context;
    }

    public override void Enter()
    {
        _context.Animator.SetBool("IsGrounded", true);
        ChangeSubState(_context.Factory.Idle(this));
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
        if(!MovementUtils.IsGrounded(_context.Rigidbody)){
            _superstate.ChangeSubState(_context.Factory.Airborne(_superstate));
            return;
        }

        if(_context.Input.IsJumping){
            _context.Rigidbody.AddForce(Vector3.up * _context.JumpForce * Time.deltaTime * 10, ForceMode.Impulse);
            _context.Animator.SetTrigger("Jump");
        }
        
    }

    
}